const _ = require('lodash');

const nextDigitCaptcha = input => {
  const digits = input.toString().split('').map(i => parseInt(i));

  return _.reduce(
    digits,
    (result, value, i) => {
      if (value === digits[(i + 1) % digits.length]) {
        return result + value;
      }
      return result;
    },
    0);
};

const halfwayDigitCaptcha = input => {
  const digits = input.toString().split('').map(i => parseInt(i));

  return _.reduce(
    digits,
    (result, value, i) => {
      if (value === digits[(i + (digits.length / 2)) % digits.length]) {
        return result + value;
      }
      return result;
    },
    0);
};

module.exports = {
  nextDigitCaptcha,
  halfwayDigitCaptcha
};