const _ = require('lodash');

const part1 = (input) => {

  let score = 0;
  let groupLevel = 0;
  let isGarbage = false;
  let shouldIgnoreNext = false;

  const enterGroup = () => {
    groupLevel++;
    score += groupLevel;
  };

  const leaveGroup = () => {
    groupLevel--;
  };

  const enterGarbage = () => {
    isGarbage = true;
  };

  const leaveGarbage = () => {
    isGarbage = false;
  };

  _.each(input, c => {
    if(shouldIgnoreNext) {
      shouldIgnoreNext = false;
      return;
    }

    if (c === '{' && !isGarbage) {
      enterGroup();
      return;
    }

    if(c === '}' && !isGarbage) {
      leaveGroup();
      return;
    }

    if(c === '<' && !isGarbage) {
      enterGarbage();
      return;
    }

    if(c === '>' && isGarbage) {
      leaveGarbage();
      return;
    }

    if(c === '!' && isGarbage) {
      shouldIgnoreNext = true;
      return;
    }

  });

  return score;
};

const part2 = (input) => {
  return input;
};

module.exports = {
  part1,
  part2,
};