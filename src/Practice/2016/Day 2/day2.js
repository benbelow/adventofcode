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
  console.log(input, n);
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

const part1 = (input) => {
  let code = '';
  const currentNumber = () => parseInt(_.last(code)) || 5;
  const instructions = parseInstructions(input);
  console.log(instructions);
  _.each(instructions, i => {
    console.log(currentNumber());
    console.log(_.reduce(i, (pos, input) => followInstruction(input, pos), currentNumber()));
    code += _.reduce(i, (pos, input) => followInstruction(input, pos), currentNumber());
    console.log(code);
    console.log(_.last(code));
  });
  console.log(code);
  return parseInt(code);
};

const part2 = (input) => {
  return input;
};

module.exports = {
  part1,
  part2,
};