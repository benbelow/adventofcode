const _ = require('lodash');


class Particle {

  constructor(id, posString, velString, accString) {
    this.id = id;
    const parse = s => _.map(s.split('<')[1].split('>')[0].split(','), p => parseInt(p));
    this.pos = parse(posString);
    this.vel = parse(velString);
    this.acc = parse(accString);
    this.minDistance = this.dist();
    this.passedMin = false;
  }

  tick() {
    this.vel = _.map(this.vel, (v, i) => v + this.acc[i]);
    this.pos = _.map(this.pos, (v, i) => v + this.vel[i]);
    if (this.dist() < this.minDistance) {
      this.minDistance = this.dist();
    } else if (
      _.filter(this.pos, (v, i) => Math.sign(v) === Math.sign(this.acc[i]) || Math.sign(this.acc[i]) === 0).length === 3
      && _.filter(this.pos, (v, i) => Math.sign(v) === Math.sign(this.vel[i]) || Math.sign(this.vel[i]) === 0).length === 3
    ) {
      this.passedMin = true;
    }
  }

  dist() {
    return _.reduce(this.pos, (sum, i) => sum + Math.abs(i), 0);
  }

  hasPassedOrigin() {
    return this.passedMin;
  }

  minAcc() {
    return _.min(_.map(this.acc, a => Math.abs(a)));
  }

  minVel() {
    return _.min(_.map(this.vel, a => Math.abs(a)));
  }

  minPos() {
    return _.min(_.map(this.pos, a => Math.abs(a)));
  }

  totalAcc() {
    return _.sum(_.map(this.acc, a => Math.abs(a)));
  }

  totalVel() {
    return _.sum(_.map(this.vel, a => Math.abs(a)));
  }

  totalPos() {
    return _.sum(_.map(this.pos, a => Math.abs(a)));
  }
}

const part1 = (input) => {

  const particles = _.map(input.split('\n'), (p, i) => {
    return new Particle(i, ...p.split(', '));
  });


  let i =0;
  const longTerm = 50;
  let minAccParticles = _.first(_.map(_.groupBy(particles, p => p.totalAcc())));
  let minVelParticles = _.first(_.map(_.groupBy(minAccParticles, p => p.totalVel())));
  let closest = _.map(_.groupBy(minVelParticles, p => p.minPos()))[0][0];
  return closest.id;
};

const part2 = (input) => {

  let particles = _.map(input.split('\n'), (p, i) => {
    return new Particle(i, ...p.split(', '));
  });

  const longTerm = 100;
  let i =0;

  const hasCollisions = particle => {
    return _.filter(particles, p2 => p2.id !== particle.id && _.isEqual(particle.pos, p2.pos)).length > 0;
  };

  while(i < longTerm) {
    _.forEach(particles, p => p.tick());
    particles = _.filter(particles, p => !hasCollisions(p));
    i++;
  }

  return particles.length;
};

module.exports = {
  part1,
  part2,
};