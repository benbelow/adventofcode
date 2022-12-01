const _ = require('lodash');

const part1 = (input) => {
  let data = [0];
  let index = 0;

  _.each(_.range(1, 2018), i => {
    index = ((index + input) % data.length) + 1;
    data.splice(index, 0, i);
  });

  console.log(data[1]);

  return data[(index + 1) % data.length];
};

const part2 = (input, upperBound = 50000000) => {
  let total = 1;
  let index = 0;
  let answer;

  _.each(_.range(1, upperBound + 1), i => {
    index = ((index + input) % total) + 1;
    total++;
    if(index === 1) {
      answer = i;
     }
  });

  console.log(answer);

  return answer;
};

module.exports = {
  part1,
  part2,
};