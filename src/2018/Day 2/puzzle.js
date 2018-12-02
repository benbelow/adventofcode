const _ = require('lodash');

const part1 = (input) => {
  const ids = input.split('\n');
  let twoLetters = 0;
  let threeLetters = 0;

  for(let id of ids) {
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
  }

  return twoLetters * threeLetters;
};

const part2 = (input) => {
  const alphabet = 'abcdefghijklmnopqrstuvwxyz';

  const ids = input.split('\n');

  for(let i = 0; i < ids.length; i++) {
    let currentId = ids[i];
    const match = ids.slice(i).find(id => differByOne(currentId, id));
    if (match) {
      return difference(currentId, match);
    }
    console.log(match);
  }
};

const differByOne = (s1, s2) => {
  if (s1.length != s2.length) {
    return false;
  }

  let differenceCount = 0;

  for(let i=0; i < s1.length; i++) {
    if (s1[i] !== s2[i]) {
      differenceCount++;
    }
  }

  return differenceCount === 1;
};

const difference = (s1, s2) => {
  return s1.split('').reduce((answer, v, i) => {
    return s2[i] === v ? answer + v : answer;
  }, '');
};

module.exports = {
  part1,
  part2,
};