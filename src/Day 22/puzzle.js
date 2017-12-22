const _ = require('lodash');

const directions = {
  UP: [0, 1],
  DOWN: [0, -1],
  LEFT: [-1, 0],
  RIGHT: [1, 0],
};

const orderedDirections = [
  directions.UP, directions.RIGHT, directions.DOWN, directions.LEFT
];

const part1 = (input, bursts = 10000) => {
  let infected = [];
  let coord = [0, 0];
  let directionIndex = 0;

  const burstNum = bursts;

  let infectionCount = 0;

  const direction = () => orderedDirections[directionIndex];

  const left = () => {
    directionIndex--;
    if (directionIndex < 0) {
      directionIndex += orderedDirections.length;
    }
  };

  const right = () => {
    directionIndex++;
    if (directionIndex >= orderedDirections.length) {
      directionIndex -= orderedDirections.length;
    }
  };

  const midpoint = (input.split('\n').length -1) / 2;

  _.forEach(input.split('\n'), (y, j) => {
    _.forEach(y.split(''), (x, i) => {
      if (x === '#') {
        console.log(i, j);
        infected.push([i - midpoint, midpoint - j]);
      }
    })
  });


  const currentNodeInfected = () => {
    return _.filter(infected, i => _.isEqual(i, coord)).length > 0;
  };

  const infect = () => {
    infectionCount++;
    if(!_.includes(infected, coord)) {
      infected.push(coord);
    }
  };

  const clean = () => {
    infected = _.filter(infected, i => !_.isEqual(i, coord))
  };

  const move = () => {
    coord = [coord[0] + direction()[0], coord[1] + direction()[1]];
  };

  const doWork = () => {
    if(currentNodeInfected()) {
      right();
      clean();
    } else {
      left();
      infect();
    }
    move();
  };

  _.forEach(_.range(burstNum), i => {
    doWork();
  });


  return infectionCount;
};

const part2 = (input, bursts = 10000000) => {
  let states = [];
  let coord = [0, 0];
  let directionIndex = 0;

  const burstNum = bursts;

  let infectionCount = 0;

  const direction = () => orderedDirections[directionIndex];

  const left = () => {
    directionIndex--;
    if (directionIndex < 0) {
      directionIndex += orderedDirections.length;
    }
  };

  const right = () => {
    directionIndex++;
    if (directionIndex >= orderedDirections.length) {
      directionIndex -= orderedDirections.length;
    }
  };

  const midpoint = (input.split('\n').length -1) / 2;

  _.forEach(input.split('\n'), (y, j) => {
    _.forEach(y.split(''), (x, i) => {
      if (x === '#') {
        states[[i - midpoint, midpoint - j]] = 2;
      } else {
        states[[i - midpoint, midpoint - j]] = 0;
      }
    })
  });

  const currentState = () => states[coord];

  const encounter = () => {
    const cs = currentState();
    if (cs === 1) {
      infectionCount++;
    }
    states[coord] = (cs + 1) % 4;
  };

  const move = () => {
    coord = [coord[0] + direction()[0], coord[1] + direction()[1]];
    if (!currentState()) {
      states[coord] = 0;
    }
  };

  const turn = () => {
    const state = currentState();

    switch(state) {
      case 0:
        left();
        break;
      case 1:
        break;
      case 2:
        right();
        break;
      case 3:
        right();
        right();
    }
  };

  const doWork = () => {
    turn();
    encounter();
    move();
  };

  _.forEach(_.range(burstNum), i => {
    doWork();
  });

  return infectionCount;
};

module.exports = {
  part1,
  part2,
};