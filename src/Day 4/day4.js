const _ = require('lodash');

const isValidPassphrase = input => {
  let words = input.split(' ');
  return _.uniq(words).length === words.length;
};

const numberOfValidPhrases = input => {
  return _.reduce(input.split('\n'), (sum, x) => {
    if (isValidPassphrase(x)) {
      return sum + 1;
    }
    return sum;
  }, 0);
};

module.exports = {
  isValidPassphrase,
  numberOfValidPhrases
};