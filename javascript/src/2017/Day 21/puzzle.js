const _ = require('lodash');

var rotateClockwise = function (matrix) {
  // reverse the rows
  matrix = matrix.reverse();
  // swap the symmetric elements
  for (var i = 0; i < matrix.length; i++) {
    for (var j = 0; j < i; j++) {
      var temp = matrix[i][j];
      matrix[i][j] = matrix[j][i];
      matrix[j][i] = temp;
    }
  }
  return matrix;
};

const flipVertical = matrix => {
  return matrix.reverse();
};

const flipHorizontal = matrix => {
  return _.map(matrix, m => m.reverse());
};

const part1 = (input, iterationNum = 5) => {
  const rules = _.map(input.split('\n'), r => {
    const parts = r.split('=>');
    const toMatrix = s => _.map(s.split('/'), x => {
      return _.map(x.trim().split(''), y => y === '#' ? 1 : 0);
    });
    return { from: toMatrix(parts[0]), to: toMatrix(parts[1]) };
  });

  let pattern = [[0, 1, 0], [0, 0, 1], [1, 1, 1]];

  const splitBy = (matrix, n) => {
    let result = [];
    for (let i = 0; i < matrix.length; i += n) {
      for (let j = 0; j < matrix.length; j += n) {
        let section = [];
        _.forEach(_.range(n), q => {
          section.push(matrix[i + q].slice(j, j + n));
        });
        result.push(section);
      }
    }
    return result;
  };

  const join = (sections) => {
    const chunkSize = sections[0].length;
    const size = (Math.sqrt(sections.length) * chunkSize);
    return _.reduce(_.chunk(sections, size / chunkSize), (res, c) => {
      _.forEach(_.range(chunkSize), i => {
        res.push(_.reduce(_.map(c, x => x[i]), (r, q) => _.concat(r, q), []))
      });
      return res
    }, []);
  };

  function* transformer(matrix) {
    var rot1 = 0;
    var rot2 = 0;
    var rot3 = 0;
    while(rot1 < 4) {
      matrix = rotateClockwise(matrix);
      rot1++;
      yield matrix;
    }
    matrix = flipHorizontal(matrix);
    while(rot2 < 4) {
      matrix = rotateClockwise(matrix);
      rot2++;
      yield matrix;
    }
    matrix = flipVertical(flipHorizontal(matrix));
    while(rot3 < 4) {
      matrix = rotateClockwise(matrix);
      rot3++;
      yield matrix;
    }
  }

  const runRule = matrix => {
    const t = transformer(matrix);
    var failsafe = 0;
    while(_.filter(_.map(rules, r => r.from), x => {
      return _.isEqual(x, matrix);
    }).length === 0 && failsafe < 30) {
      matrix = t.next().value;
      failsafe++;
    }

    const rule = _.filter(rules, r => _.isEqual(r.from, matrix))[0];

    return rule.to;
  };

  const transform = p => {
    if (p.length % 2 === 0) {
      const parts = splitBy(p, 2);
      const newParts = _.map(parts, runRule);
      return join(newParts);
    } else if (p.length % 3 === 0) {
      const parts = splitBy(p, 3);
      const newParts = _.map(parts, runRule);
      return join(newParts);
    }
  };

  for (let i = 0; i < iterationNum; i++) {
    console.log(i);
    pattern = transform(pattern);
  }

  return _.sumBy(pattern, p => _.sum(p));
};

const part2 = (input) => {
  return input;
};

module.exports = {
  part1,
  part2,
};