const _ = require('lodash');

const directions = {
  DOWN: [0, 1],
  UP: [0, -1],
  LEFT: [-1, 0],
  RIGHT: [1, 0],
};

const orderedDirections = [directions.DOWN, directions.RIGHT, directions.UP, directions.LEFT];

const mazeMatcher = /[|+\-A-Z]/;
const letterMatcher = /[A-Z]/;

const part1 = (input) => {
  const lines = _.map(input.split('\n'), x => _.map(x, _.identity()));
  let coord = [_.indexOf(lines[0], '|'), 0];
  let dir = directions.DOWN;

  let result = '';
  let finished = false;

  const charAt = c => {
    if(
      c[1] < 0
      || c[1] >= lines.length
      || c[0] < 0
      || c[0] > lines[c[1]].length
    ) {
      return null;
    }
    return lines[c[1]][c[0]];
  };

  const next = (d = dir) => {
    return [coord[0] + d[0], coord[1] + d[1]];
  };

  const changeDir = () => {
    const opposite = dir => {
      return _.map(dir, x => x*-1);
    };

    let changed = false;

    _.filter(orderedDirections, od => od !== dir && od[0] !== opposite(dir)[0] && od[1] !== opposite(dir)[1]).forEach(d => {
      // console.log(d, next(d), charAt(next(d)))
      if(mazeMatcher.test(charAt(next(d))) && !changed) {
        dir = d;
        changed = true;
      }
    });

    if(!changed) {
      finished = true;
    }
  };

  const go = () => {
    const n = next();
    console.log(coord, dir, n, charAt(n));
    if(mazeMatcher.test(charAt(n))) {
      result += letterMatcher.test(charAt(n)) ? charAt(n) : '';
      coord = n;
    } else
      changeDir();
  };

  while(!finished) {
    // console.log(coord);
    go();
  }

  console.log(result);

  return result;
};

const part2 = (input) => {

  const lines = _.map(input.split('\n'), x => _.map(x, _.identity()));
  let coord = [_.indexOf(lines[0], '|'), 0];
  let dir = directions.DOWN;

  let result = 1;
  let finished = false;

  const charAt = c => {
    if(
      c[1] < 0
      || c[1] >= lines.length
      || c[0] < 0
      || c[0] > lines[c[1]].length
    ) {
      return null;
    }
    return lines[c[1]][c[0]];
  };

  const next = (d = dir) => {
    return [coord[0] + d[0], coord[1] + d[1]];
  };

  const changeDir = () => {
    const opposite = dir => {
      return _.map(dir, x => x*-1);
    };

    let changed = false;

    _.filter(orderedDirections, od => od !== dir && od[0] !== opposite(dir)[0] && od[1] !== opposite(dir)[1]).forEach(d => {
      // console.log(d, next(d), charAt(next(d)))
      if(mazeMatcher.test(charAt(next(d))) && !changed) {
        dir = d;
        changed = true;
      }
    });

    if(!changed) {
      finished = true;
    }
  };

  const go = () => {
    const n = next();
    console.log(coord, dir, n, charAt(n));
    if(mazeMatcher.test(charAt(n))) {
      result += 1;
      coord = n;
    } else
      changeDir();
  };

  while(!finished) {
    // console.log(coord);
    go();
  }

  console.log(result);

  return result;
};

module.exports = {
  part1,
  part2,
};