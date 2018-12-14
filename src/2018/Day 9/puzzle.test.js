const part1 = require('./puzzle').part1;
const part2 = require('./puzzle').part2;

const finalInput = require('./input');

const examples = [
    [9, 25, 32],
    [10, 1618, 8317],
    [13, 7999, 146373],
    [17, 1104, 2764],
    [21, 6111, 54718],
    [30, 5807, 37305],
];

describe('Puzzle', () => {
  describe('Part 1', () => {
    describe('example cases', () => {
      it('example 1', () => {
        for(let e of examples) {
          expect(part1(e[0], e[1])).toBe(e[2]);
        }
      });
    });

    it('final input', () => {
      // expect(part1(411, 71058)).toBe(424639);
    });
  });

  describe('Part 2', () => {
    describe('example cases', () => {
      it('example 1', () => {
        // expect(part2(0)).toBe(0);
      });
    });

    it('final input', () => {
        // expect(part1(411, 7105800)).toBe(3516007333);
    });
  })
});
