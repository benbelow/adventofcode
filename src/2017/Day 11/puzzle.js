const _ = require('lodash');

const stepTypes = {
  'n': [0, 1, -1],
  'ne': [1, 0, -1],
  'se': [1, -1, 0],
  's': [0, -1, 1],
  'sw': [-1, 0, 1],
  'nw': [-1, 1, 0],
};

const part1 = (input) => {
  const steps = input.split(',');

  const position = [0, 0, 0];

  const applyStep = step => _.each(_.get(stepTypes, step), (s, i) => position[i] += s);

  _.each(steps, s => applyStep(s));

  console.log(steps);
  console.log(position);

  return _.sum(position.map(p => Math.abs(p)))/2;
};

const part2 = (input) => {
  const steps = input.split(',');

  const position = [0, 0, 0];

  const applyStep = step => _.each(_.get(stepTypes, step), (s, i) => position[i] += s);

  const distanceFromOrigin = () => _.sum(position.map(p => Math.abs(p)))/2;

  let maxDistance = 0;

  _.each(steps, s => {
    applyStep(s);
    if(distanceFromOrigin() > maxDistance) {
      maxDistance = distanceFromOrigin();
    }
  });

  return maxDistance;
};

module.exports = {
  part1,
  part2,
};