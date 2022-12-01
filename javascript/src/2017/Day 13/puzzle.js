const _ = require('lodash');

const part1 = (input) => {
  const lines = input.split('\n');
  const layers = _.map(lines, i => ({
    depth: parseInt(i.split(':')[0]),
    range: parseInt(i.split(':')[1])
  }));

  const layer = i => _.find(layers, l => l.depth === i);
  const severity = layer => layer.depth * layer.range;

  const maxDepth = _.max((_.map(layers, l => l.depth)));
  let lasers = _.map(_.range(maxDepth + 1), i => {
    return layer(i) ? 0 : -1;
  })
  let laserDirections = _.map(_.range(maxDepth + 1), i => {
    return layer(i) ? -1 : 0;
  })

  const tickLasers = () => {
    lasers = _.map(lasers, (l, i) => {
      const lay = layer(i);
      if (lay) {
        if (l === 0) {
          laserDirections[i] = 1;
        }
        else if (l === lay.range - 1) {
          laserDirections[i] = -1;
        }
        return l + laserDirections[i]
      }
      return -1;
    })
  }

  let sev = 0;

  for (let i=0; i <= maxDepth; i++) {
    if (lasers[i] === 0) {
      sev += severity(layer(i));
    }
    tickLasers();
  }

  return sev;
};

const part2 = (input) => {
    const lines = input.split('\n');
    const layers = _.map(lines, i => ({
      depth: parseInt(i.split(':')[0]),
      range: parseInt(i.split(':')[1])
    }));

    const isCaught = (delay) => {
      const test = _.map(layers, l => (delay + l.depth) % (l.range * 2 - 2));
      return _.filter(test, t => t === 0).length !== 0;
    }

    for(let i = 0; i < 999999999; i++) {
      if(!isCaught(i)) {
        return i;
      }
    }

    return -1;
};

module.exports = {
  part1,
  part2,
};
