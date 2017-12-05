const _ = require('lodash');

const generator = function* (input) {
  const inputs = _.map(input.split('\n'), i => parseInt(i));
  const modifications = _.map(inputs, i => 0);
  let index = 0;

  while (true) {
    const oldIndex = _.clone(index);
    index = index + inputs[index] + modifications[index];
    modifications[oldIndex] = modifications[oldIndex] + 1;
    yield index;
  }
};

const generator2 = function* (input) {
  const inputs = _.map(input.split('\n'), i => parseInt(i));
  const modifications = _.map(inputs, i => 0);
  let index = 0;

  while (true) {
    const oldIndex = _.clone(index);
    index = index + inputs[index] + modifications[index];
    if(inputs[oldIndex] + modifications[oldIndex] >= 3) {
      modifications[oldIndex] = modifications[oldIndex] - 1;
    } else {
      modifications[oldIndex] = modifications[oldIndex] + 1;
    }
    yield index;
  }
};

const part1 = (input) => {
  const inputs = _.map(input.split('\n'), i => parseInt(i));

  let index = 0;
  let count = 0;

  const g = generator(input);

  while ((index >= 0 && index < inputs.length)) {
    index = g.next().value;
    count += 1;
  }

  return count;
};

const part2 = (input) => {
  const inputs = _.map(input.split('\n'), i => parseInt(i));

  let index = 0;
  let count = 0;

  const g = generator2(input);

  while ((index >= 0 && index < inputs.length)) {
    index = g.next().value;
    count += 1;
  }

  return count;
};

module.exports = {
  part1,
  part2,
};