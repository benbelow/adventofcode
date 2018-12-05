const _ = require('lodash');

const part1 = (input) => {
  input = input.split('');

  do {
    input = input.filter(s => s !== '#');
    for (let i = 0; i < input.length; i++) {
      if (isMatch(input[i], input[i + 1])) {
        input[i] = '#';
        input[i + 1] = '#';
      }
    }
  } while (input.includes('#'));

  return input.length;
};

const part2 = (input) => {
  return input;
};

const isMatch = (s1, s2) => {
  return s1 && s2 && s1.toLowerCase() === s2.toLowerCase()
    && ((s1 === s1.toLowerCase() && s2 === s2.toUpperCase()) || (s1 === s1.toUpperCase() && s2 === s2.toLowerCase()))
};

module.exports = {
  part1,
  part2,
  isMatch
};