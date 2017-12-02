const _ = require('lodash');

const checksum = input => {
  const lines = _(input.split('\n'))
    .map(l => l.split(/ |\t/))
    .map(l => _.map(l, i => parseInt(i)))
    .value();
  const result = _.reduce(lines, (r, line) => {
    const sorted = line.sort((a, b) => (a - b));
    return r + (_.last(sorted) - _.first(sorted));
  }, 0)
  return result;
}

module.exports = {
  checksum,
}
