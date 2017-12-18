const _ = require('lodash');

const part1 = (input) => {
  const registers = {};
  let lastPlayed;
  let result;
  let toIgnore = 0;

  let j = 0;
  let k = 0;

  const instructions = _.map(input.split('\n'), i => {
    return i.split(' ');
  });

  const perform = ins => {

    // console.log(ins);
    const val = parseInt(ins[2]) ? parseInt(ins[2]) : registers[ins[2]];
    const reg = ins[1];

    if (!_.keys(registers).includes(ins[1])) {
      registers[ins[1]] = 0;
    }

    switch (ins[0]) {
      case 'snd':
        lastPlayed = registers[reg];
        break;
      case 'set':
        registers[reg] = val;
        break;
      case 'add':
        registers[reg] += val;
        break;
      case 'mul':
        if (parseInt(ins[2])) {
          registers[reg] *= parseInt(ins[2]);
        } else {
          registers[reg] *= registers[ins[2]];
        }
        break;
      case 'mod':
        registers[reg] = registers[reg] % val;
        break;
      case 'rcv':
        if (registers[reg] !== 0 && result === undefined) {
          result = lastPlayed;
        }
        break;
      case 'jgz':
        if (registers[reg] > 0) {
          j += (val - 1);
          // toIgnore += ((val + instructions.length) % instructions.length);
        }
    }
  };

  while (!result && k < 1400) {
    // console.log(j);
    perform(instructions[(j % instructions.length)]);
    if (_.filter(_.values(registers), v => isNaN(v)).length > 0) {
      console.log(instructions[(j % instructions.length)]);
    }
    j++;
    k++;
  }

  return result;
};

const part2 = (input) => {
  const instructions = _.map(input.split('\n'), i => {
    return i.split(' ');
  });

  let programs = [];

  function Program(id) {
    this.registers = { p: id };
    this.queue = [];
    this.sent = 0;
    this.id = id;
    this.index = 0;

    this.next = () => this.index++;

    this.getNumber = a => {
      return isNaN(a) ? this.registers[a] : parseInt(a);
    };

    this.commands = {
      'set': (a, b) => {
        this.registers[a] = this.getNumber(b);
        this.next();
      },
      'mul': (a, b) => {
        this.registers[a] *= this.getNumber(b);
        this.next();
      },
      'add': (a, b) => {
        this.registers[a] += this.getNumber(b);
        this.next();
      },
      'mod': (a, b) => {
        this.registers[a] %= this.getNumber(b);
        this.next();
      },
      'snd': a => {
        programs[(this.id + 1) % 2].queue.push(this.getNumber(a));
        this.sent++;
        this.next();
      },
      "jgz": (a, b) => { this.index += this.getNumber(a) > 0 ? this.getNumber(b) : 1; },
      'rcv': a => {
        if (this.queue.length > 0) {
          this.registers[a] = this.queue.shift();
          this.next();
        }
      }
    };

    this.execute = () => {
      const currentInstruction = instructions[this.index];
      this.commands[currentInstruction[0]](currentInstruction[1], currentInstruction[2]);
    };

    this.isStuck = () => instructions[this.index][0] === 'rcv' && this.queue.length === 0;

  }

  programs = [new Program(0), new Program(1)];

  while(_.filter(programs, p => p.isStuck()).length !== programs.length) {
    _.each(programs, p => p.execute() );
  }
  return programs[1].sent;
};

module.exports = {
  part1,
  part2,
};