const _ = require('lodash');

const factorA = 16807;
const factorB = 48271;
const divider = 2147483647;

const part1 = (initialA, initialB) => {
  function* generator(initial, factor, divider) {
    let value = initial;
    while (true) {
      value = (value * factor) % divider;
      yield value;
    }
  }

  const genA = generator(initialA, factorA, divider);
  const genB = generator(initialB, factorB, divider);

  const last16Bits = i => {
    const binary = i.toString(2);
    return binary.slice(binary.length - 16, binary.length);
  };

  const last16BitsMatch = (a, b) => {
    return last16Bits(a) === last16Bits(b);
  };

  let count = 0;
  _.each(_.range(40000000), x => {
    if(last16BitsMatch(genA.next().value, genB.next().value)) {
      count++;
    }
  });

  return count;
};

const part2 = (initialA, initialB) => {
  function* generator(initial, factor, divider, multiple) {
    let value = initial;
    while (true) {
      value = (value * factor) % divider;
      if (value % multiple === 0) {
        yield value;
      }
    }
  }

  const genA = generator(initialA, factorA, divider, 4);
  const genB = generator(initialB, factorB, divider, 8);

  const last16Bits = i => {
    const binary = i.toString(2);
    return binary.slice(binary.length - 16, binary.length);
  };

  const last16BitsMatch = (a, b) => {
    return last16Bits(a) === last16Bits(b);
  };

  let count = 0;
  _.each(_.range(5000000), x => {
    let aVal = genA.next().value;
    let bVal = genB.next().value;
    if(last16BitsMatch(aVal, bVal)) {
      count++;
    }
  });

  return count;
};

module.exports = {
  part1,
  part2,
};