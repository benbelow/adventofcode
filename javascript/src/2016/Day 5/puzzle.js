const _ = require('lodash');
const md5 = require('blueimp-md5');

const part1 = (input) => {
  let password = '';
  let i = 0;

  while (password.length < 8) {
    const hash = md5(input + i);
    if (hash.slice(0,5) === '00000') {
      password += hash[5];
    }
    i++;
  }
  return password;
};

const part2 = (input) => {
  let password = '########';
  let i = 0;

  String.prototype.replaceAt=function(index, replacement) {
    return this.substr(0, parseInt(index)) + replacement+ this.substr(parseInt(index) + replacement.length);
  };

  while (password.includes('#')) {
    const hash = md5(input + i);
    if (hash.slice(0,5) === '00000' && hash[5] < 8 && password[hash[5]] === '#') {
      password = password.replaceAt([hash[5]], hash[6]);
    }
    i++;
  }
  return password;
};

module.exports = {
  part1,
  part2,
};