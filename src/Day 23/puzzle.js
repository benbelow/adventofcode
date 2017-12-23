const _ = require('lodash');
const isPrime = require('mathjs').isPrime;

const part1 = (input) => {

  const instructions = _.map(input.split('\n'), i => {
    return i.split(' ');
  });

  let programs = [];

  function Program(id) {
    this.registers = {};
    const initial = 'abcdefgh';

    _.forEach(initial.split(''), i => this.registers[i] = 0);

    this.id = id;
    this.index = 0;

    this.mulCount = 0;

    this.next = () => this.index++;

    this.getNumber = a => {
      return isNaN(a) ? this.registers[a] : parseInt(a);
    };

    this.commands = {
      'set': (a, b) => {
        this.registers[a] = this.getNumber(b);
        this.next();
      },
      'sub': (a, b) => {
        this.registers[a] -= this.getNumber(b);
        this.next();
      },
      'mul': (a, b) => {
        this.mulCount++;
        this.registers[a] *= this.getNumber(b);
        this.next();
      },
      "jnz": (a, b) => {
        this.index += this.getNumber(a) !== 0 ? this.getNumber(b) : 1;
      },
    };

    this.execute = () => {
      const currentInstruction = instructions[this.index];
      this.commands[currentInstruction[0]](currentInstruction[1], currentInstruction[2]);
      // console.log(this.index, currentInstruction, this.registers)
    };

  }

  const program = new Program(0);
  let failsafe = 0;
  let maxFailsafe = 30000;
  while (program.index < instructions.length && program.index >= 0) {
    program.execute();
    failsafe++;
  }

  if (failsafe === maxFailsafe) {
    return -1;
  }

  return program.mulCount;
};

const part2 = (input) => {
  let res = 0;
  const b = 99 * 100 + 100000;
  const c = b + 17000;

  for(let i = b; i <= c + 1; i+=17) {
    if (!isPrime(i)) {
      res++;
    }
  }

  return res;
};

module.exports = {
  part1,
  part2,
};