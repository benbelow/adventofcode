const _ = require('lodash');

const alphabet = 'abcdefghijklmnopqrstuvwxyz';

// no checksum. Returns array of encrypted words
const removeId = function (room) {
  return room.split('-').slice(0, room.split('-').length - 1);
};

const isRealRoom = roomInput => {
  const noChecksum = roomInput.split('[')[0];
  const letters = _.reduce(removeId(noChecksum), (sum, x) => sum + x, '');
  const orderedLetterArray = letters.split('').sort();
  const orderedGroups = _.sortBy(_.groupBy(orderedLetterArray), (k, v) => -1 * k.length);
  const checksum = _.reduce(orderedGroups.slice(0,5), (v, x) => v + x[0], '');

  const givenChecksum = (function (roomInput) {
  return roomInput.split('[')[1].split(']')[0];
})(roomInput);

  return checksum === givenChecksum;
};

const unencryptLetter = (l, roomId) => {
  return alphabet[(_.indexOf(alphabet, l) + parseInt(roomId)) % (alphabet.length)];
};

// no checksum
const unencryptRoom = room => {
  const id = _.last(room.split('-'));
  const name = removeId(room);
  const words = _.map(name, w => w.split('').map(l => unencryptLetter(l, id)));
  return(_.reduce(words, (s, w) => `${s} ${_.reduce(w, (s, l) => s + l, '')}`, '')).trim();
};

const part1 = (input) => {
  const rooms = input.split('\n');
  const roomId = room => _.last(room.split('[')[0].split('-'));

  return _.reduce(_.filter(rooms, r => isRealRoom(r)),
    (sum, r) => sum + parseInt(roomId(r)),
    0);
};

const part2 = (input) => {
  const rooms = input.split('\n');
  const validRooms = _.filter(rooms, r => isRealRoom(r));

  const result = _.filter(validRooms, r => unencryptRoom(r).includes('northpole object storage'));
  return _.last(result[0].split('[')[0].split('-'));
};

module.exports = {
  part1,
  part2,
  isRealRoom,
  unencryptRoom,
};