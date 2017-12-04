const _ = require('lodash');

const clockwiseDirections = [{x: 0, y: 1}, {x: 1, y: 0}, {x: 0, y: -1}, {x: -1, y: 0}];

let parseInstructions = function (input) {
  return _.map(input.split(', '), i => {
    const direction = i.slice(0, 1);
    const amount = i.slice(1);
    return { direction, amount: parseInt(amount) };
  });
};

const equalCoords = (c1, c2) => {
  return c1.x === c2.x && c1.y === c2.y;
};

const numberOfBlocksAway = (input) => {
  const instructions = parseInstructions(input);

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
      return {x: total.x + (i.amount * direction().x), y: total.y + (i.amount * direction().y)}
    },
    { x: 0, y: 0 }
  );

  return Math.abs(final.x) + Math.abs(final.y);
};

const part2 = (input) => {
  const instructions = parseInstructions(input);

  let currentDirectionIndex = 0;
  const direction = () => clockwiseDirections[currentDirectionIndex];

  const visitedLocations = [{x: 0, y: 0}];
  let solution;

  const final = _.reduce(
    instructions,
    (total, i) => {
      if (solution) {
        return solution;
      }

      switch(i.direction) {
        case 'L':
          currentDirectionIndex = (currentDirectionIndex + clockwiseDirections.length - 1) % clockwiseDirections.length;
          break;
        case 'R':
          currentDirectionIndex = (currentDirectionIndex + clockwiseDirections.length + 1) % clockwiseDirections.length;
          break;
      }

      for(let x=0; x < i.amount; x+=1) {
        let newCoord = {x: _.last(visitedLocations).x + direction().x, y: _.last(visitedLocations).y + direction().y};
        if(_.filter(visitedLocations, l => equalCoords(l, newCoord)).length > 0) {
          solution = newCoord;
          return solution;
        }
        visitedLocations.push(newCoord);
      }
      return _.last(visitedLocations);
    },
    { x: 0, y: 0 }
  );

  return Math.abs(final.x) + Math.abs(final.y);
};

module.exports = {
  part1: numberOfBlocksAway,
  part2,
};