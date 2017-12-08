const _ = require('lodash');

const instructionTypes = {
  inc: (x, o) => x+=o,
  dec: (x, o) => x-=o,
};

const part1 = (input) => {
  const lines = input.split('\n');

  const instructions = _.map(lines, l => {
    let splitOnSpaces = l.split(' ');
    return {
      register: splitOnSpaces[0],
      operator: _.get(instructionTypes, splitOnSpaces[1]),
      operand: splitOnSpaces[2],
      condition: _.reduce(l.split(' ').slice(4, l.length), (s, v) => `${s} ${v}`),
    };
  });

  const registry = _.merge({}, ..._.map(instructions, i => ({[i.register]: 0})));

  _.each(instructions, i => {
    if(eval(registry[i.condition.split(' ')[0]] + _.reduce(_.drop(i.condition.split(' ')), (s, v) => `${s} ${v}`, '')  )) {
      registry[i.register] = i.operator(registry[i.register], parseInt(i.operand))
    }
  });

  console.log(instructions);
  console.log(registry);

  return _.max(_.values(registry))
};

const part2 = (input) => {

  const lines = input.split('\n');

  const instructions = _.map(lines, l => {
    let splitOnSpaces = l.split(' ');
    return {
      register: splitOnSpaces[0],
      operator: _.get(instructionTypes, splitOnSpaces[1]),
      operand: splitOnSpaces[2],
      condition: _.reduce(l.split(' ').slice(4, l.length), (s, v) => `${s} ${v}`),
    };
  });

  const registry = _.merge({}, ..._.map(instructions, i => ({[i.register]: 0})));

  let currentMax = 0;


  _.each(instructions, i => {
    if(eval(registry[i.condition.split(' ')[0]] + _.reduce(_.drop(i.condition.split(' ')), (s, v) => `${s} ${v}`, '')  )) {
      registry[i.register] = i.operator(registry[i.register], parseInt(i.operand))
    }
    currentMax = Math.max(currentMax, _.max(_.values(registry)))
  });

  return currentMax;
};

module.exports = {
  part1,
  part2,
};