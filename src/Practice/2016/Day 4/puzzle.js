const _ = require('lodash');

const isRealRoom = roomInput => {
  const noChecksum = roomInput.split('[')[0];
  const letters = _.reduce(noChecksum.split('-').slice(0, noChecksum.split('-').length-1), (sum, x) => sum + x, '');
  const orderedLetterArray = letters.split('').sort();
  const orderedGroups = _.sortBy(_.groupBy(orderedLetterArray), (k, v) => -1 * k.length);
  const checksum = _.reduce(orderedGroups.slice(0,5), (v, x) => v + x[0], '');

  console.log(roomInput.split('['));
  const givenChecksum = roomInput.split('[')[1].split(']')[0];

  return checksum === givenChecksum;
};

const part1 = (input) => {
  const rooms = input.split('\n');

  const roomId = room => _.last(room.split('[')[0].split('-'));

  return _.reduce(_.filter(rooms, r => isRealRoom(r)),
    (sum, r) => sum + parseInt(roomId(r)),
    0);
};

const part2 = (input) => {
  return input;
};

module.exports = {
  part1,
  part2,
  isRealRoom,
};