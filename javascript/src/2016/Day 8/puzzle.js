const _ = require('lodash');

class Grid {
  constructor() {
    this.rows = _.map(_.range(0, 6), row => _.map(_.range(0, 50), col => 0));
    this.operations = {
      rect: (x, y) => {
        _.each(_.range(0, x), xx => {
          _.each(_.range(0, y), yy => {
            this.rows[yy][xx] = 1;
          })
        });
        return this;
      },
      rotateRow: (y, amount) => {
        const oldRow = _.clone(this.rows[y]);
        this.rows[y] = _.map(this.rows[y], (v, i) => oldRow[(i + oldRow.length - amount) % oldRow.length]);
        return this;
      },
      rotateColumn: (x, amount) => {
        const oldCol = _.clone(_.map(this.rows, r => r[x]));
        _.each(this.rows, (r, i) => {
          r[x] = oldCol[(i + oldCol.length - amount) % oldCol.length];
        });
        return this;
      },
    };
  }

  prettyPrint() {
    console.log(_.reduce(this.rows, (grid, row) => `${grid}${_.reduce(row, (s, v) => s + (v ? '|' : ' '), '')}\n`, ''));
    return this;
  }

  total() {
    return _.sumBy(this.rows, r => _.sum(r));
  }
}

const part1 = (input) => {
  const grid = new Grid();

  const lines = input.split('\n');
  const instructions = _.map(lines, i => {
    if (i.split(' ')[0] === 'rect') {
      return {
        operation: grid.operations.rect,
        operand1: i.split(' ')[1].split('x')[0],
        operand2: i.split(' ')[1].split('x')[1],
      }
    } else if (i.split(' ')[1] === 'column') {
      return {
        operation: grid.operations.rotateColumn,
        operand1: i.split('x=')[1].split(' ')[0],
        operand2: i.split('by ')[1],
      }
    } else if (i.split(' ')[1] === 'row') {
      return {
        operation: grid.operations.rotateRow,
        operand1: i.split('y=')[1].split(' ')[0],
        operand2: i.split('by ')[1],
      }
    }
  });

  _.each(instructions, i => {
    i.operation(i.operand1, i.operand2);
  });

  grid.prettyPrint();

  return grid.total();
};

const part2 = (input) => {
  return input;
};

module.exports = {
  part1,
  part2,
};