const _ = require('lodash');

const part1 = (input) => {
  const words = input.split('\n');
  const length = words[0].length;
  let result = '';

  for(let i=0; i < length; i++) {
    let groupedLetters = _.groupBy(_.map(words, w => w[i]));
    result += _.first(_.sortBy(groupedLetters, g => -g.length))[0];
  }

  return result;
};

const part2 = (input) => {
  return input;
};

module.exports = {
  part1,
  part2,
};