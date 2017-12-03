const _ = require('lodash');

const spiral = input => {
  if (input <= 1) {
    return 0;
  }
  const ring = highestSquareRootLowerThan(input);
  const recursiveInput = input - (lowestSquareRootHigherThan(input) * 4 - 4 - 1);
  return distanceToCenterOfRing(input) + ((ring - 1) / 2);
};

const distanceToCenterOfRing = input => {
  const ring = highestSquareRootLowerThan(input);
  const ringSquared = ring ** 2;
  const firstFulcrum = Math.round(ringSquared - (ring/2));
  const fulcrums = [
    firstFulcrum,
    firstFulcrum - (ring - 1),
    firstFulcrum - ((ring - 1) * 2),
    firstFulcrum - ((ring - 1) * 3),
  ];

  return Math.min(..._.map(fulcrums, f => Math.abs(f - input)))
}

// returns the highest (odd) square number's root lower than or equal to the input
const highestSquareRootLowerThan = input => {
  let i = 1;
  while (true) {
    if (i ** 2 >= input) {
      return i;
    }
    i += 2;
  }
};

// Returns the lowest (odd) square number higher than or equal to the input
const lowestSquareRootHigherThan = input => {
  let i = 1;
  while (true) {
    if (i ** 2 >= input) {
      return i;
    }
    i += 2;
  }
};

const stressTester = function* () {
  let coord = [0,0];
}

const firstLargerValueThan = input => {
  var result = stressTester();
  while (result <= input) {
    result = stressTester();
  }

  return result;
}

module.exports = {
  spiral,
  lowestSquareRootHigherThan,
  highestSquareRootLowerThan,
  distanceToCenterOfRing,
  firstLargerValueThan,
}
