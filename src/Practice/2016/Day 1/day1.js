const _ = require('lodash');

const numberOfBlocksAway = (input) => {
  const instructions = _.map(input.split(', '), i => {
    const direction = i.slice(0, 1);
    const amount = i.slice(1);
    return { direction, amount: parseInt(amount)};
  });

  const clockwiseDirections = [{x: 0, y: 1}, {x: 1, y: 0}, {x: 0, y: -1}, {x: -1, y: 0}];
  let currentDirectionIndex = 0;
  const direction = () => clockwiseDirections[currentDirectionIndex];

  const final = _.reduce(
    instructions,
    (total, i) => {
      switch(i.direction) {
        case 'L':
          currentDirectionIndex = (currentDirectionIndex + clockwiseDirections.length + 1) % clockwiseDirections.length;
          break;
        case 'R':
          currentDirectionIndex = (currentDirectionIndex + clockwiseDirections.length - 1) % clockwiseDirections.length;
          break;
      }
      console.log(currentDirectionIndex);
      return {x: total.x + (i.amount * direction().x), y: total.y + (i.amount * direction().y)}
    },
    { x: 0, y: 0 }
  );

  return Math.abs(final.x) + Math.abs(final.y);

};

const part2 = (input) => {
  return input;
};

module.exports = {
  part1: numberOfBlocksAway,
  part2,
};