const _ = require('lodash');

const part1 = (input) => {

    let index = 0;
    let values = { '0': 0 };
    let state = 'A';


    const instructions = {
      write: x => () => {
        values[index] = x
      },
      move: dir => () => {
        const x = dir === 'right' ? 1 : -1;
        index += x;
      },
      cont: s => () => {
        state = s
      },
    };

    const inputs = input.split('In state');

    const stepNum = parseInt(inputs[0].split('after ')[1].split(' ')[0]);
    const steps = _.map(inputs.slice(1, inputs.length), s => {
      const insts = s.split('current value is');
      return {
        stateCondition: s.split(':')[0].trim(),
        instructions: _.map(insts.slice(1, insts.length), i => {
          const lines = i.split('\n');
          return {
            condition: lines[0].split(':')[0].trim(),
            commands: _.filter(_.map(lines.slice(1, lines.length), c => {
              if (c.includes('Continue with state')) {
                const newState = (c.split('Continue with state')[1].split('.')[0].trim());
                return instructions.cont(newState);
              }
              else if (c.includes('Move')) {
                const newDir = c.split('to the ')[1].split('.')[0];
                return instructions.move(newDir)
              }
              else if (c.includes('Write')) {
                const newVal = c.split('Write the value ')[1].split('.')[0];
                return instructions.write(parseInt(newVal));
              }
              else {
                // console.log(c);
              }
            })),
          };
        }),
      };
    });

    const run = () => {
      const step = _.find(steps, s => s.stateCondition === state);
      const instruction = _.find(step.instructions, s => {
        if (values[index.toString()] === undefined) {
          values[index.toString()] = 0
        }
        return s.condition.toString() === values[index.toString()].toString();
      }).commands;
      _.forEach(instruction, i => {
        i();
      })
    };

    _.forEach(_.range(stepNum), () => {
      run();
    });

    return _.filter(values, v => v === 1).length;
  }
;

const part2 = (input) => {
  return input;
};

module.exports = {
  part1,
  part2,
};