const _ = require('lodash');
const knotHash = require('../Day 10/puzzle').part2;

const hashToBinary = hash => {
  return _.reduce(_.map(hash, c => parseInt(c, 16).toString(2)), (s, i) => {
    console.log(i);
    return s + '0'.repeat(4 - i.length) + i;
  }, '');
}

const part1 = (input) => {
  let count = 0;

  for(let i = 0; i < 128; i++) {
    const hash = knotHash(`${input}-${i}`);
    const hashBin = hashToBinary(hash);
    count += _.filter(hashBin, c => {
      return c === '1'
    }).length;
  }

  return count;
};

const cachedPart1 = cache => {
  let count = 0;
  const rows = eval(cache);
  return _.reduce(rows, (sum, r) => sum + _.filter(r, i => i==='1').length, 0);
}

const part2 = (cache) => {
  const rows = eval(cache);
  console.log(_.map(rows, r => _.map(r)));

  return rows.length;
};

module.exports = {
  part1,
  part2,
  cachedPart1,
};
