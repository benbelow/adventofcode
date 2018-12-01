const _ = require('lodash');

const part1 = (input) => {
  const instructions = input.split(',').map(i => i.trim());
  return eval(0 + instructions.join(' '))
};

const part2 = (input) => {
  const instructions = input.split(/\n/g);
  const seen = {0: true};
  let frequency = 0;

  while (true) {
    for (const i of instructions) {
      frequency += Number(i);
      if (seen[frequency]) {
        return frequency;
      }
      seen[frequency] = true;
    }
  }
};

module.exports = {
  part1,
  part2,
};