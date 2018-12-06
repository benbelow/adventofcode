const _ = require('lodash');

const isValidTriangle = (x, y, z) => {
  x = parseInt(x);
  y = parseInt(y);
  z = parseInt(z);
  return x + y > z && x + z > y && y + z > x;
};

function chunkArray(arr, chunkSize) {
  const chunks = [];
  while(arr.length) {
    const chunk = arr.slice(0, chunkSize);
    chunks.push(chunk);
    arr = arr.slice(chunkSize);
  }
  return chunks;
}

const part1 = (input) => {
  const triangles = _.map(input.split('\n'), t => t.trim().split(/ +/));
  return _.filter(_.map(triangles, t => isValidTriangle(...t))).length;
};

const part2 = (input) => {
  const triangles = _.map(input.split('\n'), t => t.trim().split(/ +/));
  let actualTriangles = chunkArray(_(triangles).unzip().flatten().value(), 3);
  return _.filter(_.map(actualTriangles, t => isValidTriangle(...t))).length;
};

module.exports = {
  part1,
  part2,
  isValidTriangle,
};