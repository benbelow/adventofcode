const _ = require('lodash');

const part1 = (input) => {
  const ids = input.split('\n');
  var twoLetters = 0;
  var threeLetters = 0;

  for(var id of ids) {
    const stats = id.split('').reduce((stats, v) => {
      if(_.keys(stats).includes(v)) {
        return {...stats, [v]: stats[v] + 1}
      }
      else {
        return {...stats, [v]: 1}
      }
    }, {});

    if (_.values(stats).includes(2)) {
      twoLetters++;
    }

    if (_.values(stats).includes(3)) {
      threeLetters++;
    }

    console.log(id, stats);
  }

  return twoLetters * threeLetters;
};

const part2 = (input) => {
  return input;
};

module.exports = {
  part1,
  part2,
};