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

const directions = {
  LEFT: [-1, 0],
  UP: [0, 1],
  RIGHT: [1, 0],
  DOWN: [0, -1],
}

const stressTester = function* () {
  const values = [];

  let currentDirection = 0;
  const directionOrder = [directions.RIGHT, directions.UP, directions.LEFT, directions.DOWN];
  const changeDirection = () => currentDirection = (currentDirection + 1) % 4;

  let index = 1;
  let coord = [0,0];
  const x = () => coord[0];
  const y = () => coord[1];

  const ring = () => highestSquareRootLowerThan(index);
  // Starts at 0
  const ringIndex = () => (ring() - 1) / 2;

  // moves in current Direction
  const move = () => {
    index += 1;
    coord[0] = coord[0] + directionOrder[currentDirection][0];
    coord[1] = coord[1] + directionOrder[currentDirection][1];
    calculateValue();
  }

  const calculateValue = () => {
    const xDiff = v => Math.abs(v.x - x());
    const yDiff = v => Math.abs(v.y - y());

    const newValue = _(values)
    .filter(v => (
         (xDiff(v) === 1 && yDiff(v) === 1)
      || (xDiff(v) === 1 && yDiff(v) === 0)
      || (xDiff(v) === 0 && yDiff(v) === 1)
    ))
    .reduce((sum, v) => sum + v.value, 0)

    values.push({x: x(), y: y(), value: newValue})
  }

  // base case
  values.push({x: x(), y: y(), value: 1})
  yield(_.last(values).value);

  while(true) {
    move();
    if (
      !(x() === ringIndex() && y() === (-1 * ringIndex()))
      && (
          directionOrder[currentDirection] === directions.RIGHT && x() >= ringIndex()
          || directionOrder[currentDirection] === directions.UP && y() === ringIndex()
          || directionOrder[currentDirection] === directions.LEFT && x() === (-1 * ringIndex())
          || directionOrder[currentDirection] === directions.DOWN && y() === (-1 * ringIndex())
      )
    ) {
      changeDirection();
    }

    yield(_.last(values).value);
  }
}

const firstLargerValueThan = input => {
  var st = stressTester();
  var result = st.next().value;
  while (result <= input) {
    result = st.next().value;
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
