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

  console.log(sev);
  return sev;
};

const part2 = (input) => {
  return input;
};

module.exports = {
  part1,
  part2,
};
