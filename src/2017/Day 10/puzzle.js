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
  const padding = [17, 31, 73, 47, 23];
  const lengths = _.concat(_.map(input, i => i.charCodeAt(0)), padding);

  let list = _.range(0, 256);
  let currentPosition = 0;
  let skipSize = 0;

  const wrappedIndex = i => (i + list.length) % list.length;

  _.each(_.range(0, 64), j => {
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
  });

  let denseHash = [];

  for (let i = 0; i < list.length; i += 16) {
    denseHash.push(_.reduce(list.slice(i, i + 16), (f, x) => f ^ x));
  }

  return _.reduce(denseHash.map(x => x.toString(16)).map(x => x.length === 1 ? `0${x}` : x), (s, x) => s + x);
};

module.exports = {
  part1,
  part2,
};