const _ = require('lodash');

const checksum = comparator => {
  return input => {
    const lines = _(input.split('\n'))
      .map(l => l.split(/ |\t/))
      .map(l => _.map(l, i => parseInt(i)))
      .value();
    const result = _.reduce(lines, (r, line) => {
      return r + comparator(line);
    }, 0)
    return result;
  }
}

const difference = input => {
  const sorted = input.sort((a, b) => (a - b));
  return _.last(sorted) - _.first(sorted);
}

const divisors = input => {
  const sorted = input.sort((a,b) => (b-a));
  for(var i = 0; i < sorted.length; i++) {
    for(var j = i + 1; j < sorted.length; j++) {
      console.log(sorted[i], sorted[j]);
      console.log(sorted[i] % sorted[j])
      if (sorted[i] % sorted[j] === 0) {
        console.log('return')
        return sorted[i] / sorted[j];
      }
    }
  }
  return -1;
}

module.exports = {
  checksum: checksum(difference),
  divisors,
  difference,
  checksum2: checksum(divisors),
}
