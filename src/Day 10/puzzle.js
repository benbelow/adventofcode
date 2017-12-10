const _ = require('lodash');

const part1 = (input, listSize = 256) => {
  let list = _.range(0, listSize);
  let currentPosition = 0;
  let skipSize = 0;

  const lengths = input.split(/,/).map(l => parseInt(l));

  const wrappedIndex = i => (i + list.length) % list.length;

  _.each(lengths, l => {
    const originalList = _.clone(list);

    if(l > list.length) {
      return;
    }

    _.each(_.range(0, l), i => {
      list[wrappedIndex(i + currentPosition)] = originalList[wrappedIndex(l - 1 - i + currentPosition)]
    });

    currentPosition += (l + skipSize);
    skipSize++;
  });

  return list[0] * list[1];
};

const part2 = (input) => {
  return input;
};

module.exports = {
  part1,
  part2,
};