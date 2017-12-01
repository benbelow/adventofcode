const _ = require('lodash');

const captcha = input => {
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

module.exports = {
  captcha,
};