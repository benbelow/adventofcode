const _ = require('lodash');

const isValidPassphrase = input => {
  let words = input.split(' ');
  return _.uniq(words).length === words.length;
};

const isValidPartTwoPassphrase = input => {
  let words = input.split(' ');
  console.log(words);
  return _.uniq(_.map(words, w => w.split('').sort().join())).length === words.length;
};

const numberOfValidPhrases = (input, predicate) => {
  return _.reduce(input.split('\n'), (sum, x) => {
    if (predicate(x)) {
      return sum + 1;
    }
    return sum;
  }, 0);
};

module.exports = {
  isValidPassphrase,
  numberOfValidPhrases,
  isValidPartTwoPassphrase
};