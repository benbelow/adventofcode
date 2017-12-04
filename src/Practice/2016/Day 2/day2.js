const _ = require('lodash');

const parseInstructions = input => {
  return _.map(input.split('\n'), l => l.split(''));
};

const topRow = n => {
  return n > 0 && n < 4;
};

const bottomRow = n => {
  return n > 6 && n < 10;
};

const leftColumn = n => {
  return n % 3 === 1;
};

const rightColumn = n => {
  return n % 3 === 0;
};

const U = n => {
  return topRow(n) ? n : n - 3;
};

const D = n => {
  return bottomRow(n) ? n : n + 3;
};

const L = n => {
  return leftColumn(n) ? n : n - 1;
};

const R = n => {
  return rightColumn(n) ? n : n + 1;
};

const followInstruction = (input, n) => {
  switch(input) {
    case 'U':
      return U(n);
    case 'L':
      return L(n);
    case 'D':
      return D(n);
    case 'R':
      return R(n);
  }
};

const followInstruction2 = (input, n) => {
  switch(input) {
    case 'U':
      return U2(n);
    case 'L':
      return L2(n);
    case 'D':
      return D2(n);
    case 'R':
      return R2(n);
  }
};

const U2 = n => {
  if([1, 2, 4, 5, 9].includes(n)) {
    return n;
  } else if ([13, 3].includes(n)) {
    return n-2;
  } else if ([6,7,8,10,11,12].includes(n)) {
    return n-4;
  }
};

const L2 = n => {
  if([1,2,5,10,13].includes(n)) {
    return n;
  } else {
    return n-1;
  }
};

const D2 = n => {
  if([5,9,10,12,13].includes(n)) {
    return n;
  } else if ([1,11].includes(n)) {
    return n+2;
  } else if ([6,7,8,2,3,4].includes(n)) {
    return n+4;
  }
};

const R2 = n => {
  if([1,4,9,12,13].includes(n)) {
    return n;
  } else {
    return n+1;
  }
};

const part1 = (input) => {
  let code = '';
  const currentNumber = () => parseInt(_.last(code)) || 5;
  const instructions = parseInstructions(input);
  _.each(instructions, i => {
    code += _.reduce(i, (pos, input) => followInstruction(input, pos), currentNumber());
  });
  return parseInt(code);
};

const part2 = (input) => {
  let code = [];
  const currentKey = () => _.last(code) ? _.last(code) : 5;
  const instructions = parseInstructions(input);
  _.each(instructions, i => {
    code.push(_.reduce(i, (pos, input) => {
      return followInstruction2(input, pos)}, currentKey()));
  });
  return _.reduce(code, (s, c) => s + c.toString(16), '');
};

module.exports = {
  part1,
  part2,
};