const _ = require('lodash');

const part1 = (input) => {
  const ports = _.map(input.split('\n'), i => _.map(i.split('/'), q => parseInt(q)));
  const pins = 0;

  const options = (index, taken = []) => {
    return _.filter(ports, (p, i) => (p[0] === index || p[1] === index) && !taken.includes(i))
  };

  const chains = [];

  const calculateChain = (index, taken = []) => {
    chains.push(taken);
    _.forEach(options(index, taken), (o) => {
      const next = o[0] === o[1] ? o[1] : _.find(o, i => i !== index);
      calculateChain(next, _.concat([], taken, [_.indexOf(ports, o)]))
    });
  };

  calculateChain(pins, []);

  const sums = _.map(chains, (c, j) => _.reduce(c, (sum, i) => sum + ports[i][0] + ports[i][1], 0));
  return _.max(sums);
};

const part2 = (input) => {
  const ports = _.map(input.split('\n'), i => _.map(i.split('/'), q => parseInt(q)));
  const pins = 0;

  const options = (index, taken = []) => {
    return _.filter(ports, (p, i) => (p[0] === index || p[1] === index) && !taken.includes(i))
  };

  const chains = [];

  const calculateChain = (index, taken = []) => {
    chains.push(taken);
    _.forEach(options(index, taken), (o) => {
      const next = o[0] === o[1] ? o[1] : _.find(o, i => i !== index);
      calculateChain(next, _.concat([], taken, [_.indexOf(ports, o)]))
    });
  };

  calculateChain(pins, []);

  const lengths = _.map(chains, (c, i) => [i, c.length]);
  const maxLength = _.maxBy(lengths, l => l[1])[1];

  const sums = _.map(_.filter(chains, c => c.length === maxLength), (c, j) => _.reduce(c, (sum, i) => sum + ports[i][0] + ports[i][1], 0));

  return _.max(sums);
};

module.exports = {
  part1,
  part2,
};