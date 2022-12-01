const _ = require('lodash');

const decode = (input, comparator) => {
  const words = input.split('\n');
  const length = words[0].length;
  let result = '';

  for(let i=0; i < length; i++) {
    let groupedLetters = _.groupBy(_.map(words, w => w[i]));
    result += _.first(_.sortBy(groupedLetters, comparator))[0];
  }

  return result;
};

const part1 = (input) => {
  return decode(input, x => -x.length);
};

const part2 = (input) => {
  return decode(input, x => x.length);
};

module.exports = {
  part1,
  part2,
};