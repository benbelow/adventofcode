const _ = require('lodash');

const part1 = input => {
  let numberToIgnore = 0;
  return _.reduce(input, (sum, c, i) => {
    if (numberToIgnore > 0) {
      numberToIgnore--;
      return sum;
    }

    if (c === '(') {
      const marker = input.slice(i, input.length).split('(')[1].split(')')[0];
      const characters = parseInt(marker.split('x')[0]);
      const repetitions = parseInt(marker.split('x')[1]);

      // Add one for the closing bracket. Already accounted for opening bracket
      numberToIgnore += (marker.length + 1) + characters;
      return sum + (characters * repetitions);
    }

    return sum + 1;
  }, 0)
};

const part2 = (input) => {
  return input;
};

module.exports = {
  part1,
  part2,
};