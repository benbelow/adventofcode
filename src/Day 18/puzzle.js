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

  const perform  = ins => {

    // console.log(ins);
    const val = parseInt(ins[2]) ? parseInt(ins[2]) : registers[ins[2]];
    const reg = ins[1];

    if (!_.keys(registers).includes(ins[1])) {
      registers[ins[1]] = 0;
    }

    switch(ins[0]) {
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
        if(parseInt(ins[2])) {
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
        if(registers[reg] > 0) {
          j += (val - 1);
          // toIgnore += ((val + instructions.length) % instructions.length);
        }
    }
  };

  while(!result && k < 1400) {
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
  const registersA = {};
  const registersB = {};

  const qA = [];
  const qB = [];

  let stuckA = false;
  let stuckB = false;

  let lastPlayed;
  let result = 0;

  let jA = 0;
  let jB = 0;
  let k = 0;

  const instructions = _.map(input.split('\n'), i => {
    return i.split(' ');
  });

  const performA = ins => {
    if (stuckA) {
      console.log('A: ', ins, k);
    }

    if (!_.keys(registersA).includes(ins[1])) {
      registersA[ins[1]] = 0;
    }

    const val = !isNaN(parseInt(ins[2])) ? parseInt(ins[2]) : registersA[ins[2]];
    const val1 = !isNaN(parseInt(ins[1])) ? parseInt(ins[1]) : registersA[ins[1]];
    const reg = ins[1];

    switch(ins[0]) {
      case 'snd':
        qB.unshift(val1);
        break;
      case 'set':
        if (isNaN(val)) {
          console.log(parseInt('a'));
          console.log(ins);
        }
        registersA[reg] = val;
        break;
      case 'add':
        registersA[reg] += val;
        break;
      case 'mul':
        if(parseInt(ins[2])) {
          registersA[reg] *= parseInt(ins[2]);
        } else {
          registersA[reg] *= registersA[ins[2]];
        }
        break;
      case 'mod':
        registersA[reg] = registersA[reg] % val;
        break;
      case 'rcv':
        if (qA.length > 0) {
          stuckA = false;
          registersA[reg] = qA.pop();
        } else {
          stuckA = true;
          jA--;
        }
        break;
      case 'jgz':
        if(registersA[reg] > 0) {
          jA += (val - 1);
          // toIgnore += ((val + instructions.length) % instructions.length);
        }
    }
  };


  const performB = ins => {
    if (stuckB) {
      console.log('B: ', ins, k);
    }

    if (!_.keys(registersB).includes(ins[1])) {
      registersB[ins[1]] = 0;
      if (ins[1] === 'p') {
        registersB[ins[1]] = 1;
      }
    }

    const val = !isNaN(parseInt(ins[2])) ? parseInt(ins[2]) : registersB[ins[2]];
    const val1 = !isNaN(parseInt(ins[1])) ? parseInt(ins[1]) : registersB[ins[1]];
    const reg = ins[1];

    switch(ins[0]) {
      case 'snd':
        result++;
        qA.unshift(val1);
        break;
      case 'set':
        registersB[reg] = val;
        break;
      case 'add':
        registersB[reg] += val;
        break;
      case 'mul':
        if(parseInt(ins[2])) {
          registersB[reg] *= parseInt(ins[2]);
        } else {
          registersB[reg] *= registersB[ins[2]];
        }
        break;
      case 'mod':
        registersB[reg] = registersB[reg] % val;
        break;
      case 'rcv':
        if (qB.length > 0) {
          stuckB = false;
          registersB[reg] = qB.pop();
        } else {
          stuckB = true;
          jB--;
        }
        break;
      case 'jgz':
        if(registersB[reg] > 0) {
          jB += (val - 1);
        }
    }
  };

  const kMax = 20000;

  while(!(stuckA && stuckB) && k < kMax) {
    if (jA < 0 || jB < 0) {
      console.log('oops');
    }
    performA(instructions[(jA % instructions.length)]);
    performB(instructions[(jB % instructions.length)]);
    if (_.filter(_.values(registersB), v => isNaN(v)).length > 0) {
      console.log(registersB);
      console.log(instructions[(jB % instructions.length)]);
    }
    if (_.filter(_.values(registersA), v => isNaN(v)).length > 0) {
      console.log(registersA);
      console.log(instructions[(jA % instructions.length)]);
    }
    jA++;
    jB++;
    k++;
  }

  if (k === kMax) {
    console.log(k);
    return -1;
  }

  return result;
};

module.exports = {
  part1,
  part2,
};