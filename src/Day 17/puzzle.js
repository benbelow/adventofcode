const _ = require('lodash');

const part1 = (input) => {
  let data = [0];
  let index = 0;

  _.each(_.range(1, 2018), i => {
    index = ((index + input) % data.length) + 1;
    data.splice(index, 0, i);
  });

  // console.log(data, index, data[index + 1]);

  return data[(index + 1) % data.length];
};

const part2 = (input) => {
  let data = [0];
  let index = 0;

  _.each(_.range(1, 50000000), i => {
    index = ((index + input) % data.length) + 1;
    data.splice(index, 0, i);
  });

  // console.log(data, index, data[index + 1]);

  return data[(_.indexOf(data, 0) + 1) % data.length];
};

module.exports = {
  part1,
  part2,
};