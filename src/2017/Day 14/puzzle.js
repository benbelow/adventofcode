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
  const rows = (_.map(eval(cache), r => _.map(r, i => parseInt(i))));

  const floodFill = coord => {
    if(coord[1] >= rows.length
      || coord[1] < 0
      || coord[0] >= rows[coord[1]].length
      || coord[0] < 0
      || rows[coord[1]][coord[0]] === 0) {
      return;
    }
    rows[coord[1]][coord[0]] = 0;
    floodFill([coord[0], coord[1] + 1]);
    floodFill([coord[0], coord[1] - 1]);
    floodFill([coord[0] + 1, coord[1]]);
    floodFill([coord[0] - 1, coord[1]]);
  }

  let coord = [0,0];
  let groups = 0;

  for(let y = 0; y < rows.length; y++) {
    for(let x = 0; x < rows[y].length; x++) {
      if(rows[y][x] === 1) {
        groups++;
        floodFill([x,y]);
      }
    }
  }

  return groups;
};

module.exports = {
  part1,
  part2,
  cachedPart1,
};
