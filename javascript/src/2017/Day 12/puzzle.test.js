const part1 = require('./puzzle').part1;
const part2 = require('./puzzle').part2;

const finalInput = require('./input');

const example1 = `0 <-> 2
1 <-> 1
2 <-> 0, 3, 4
3 <-> 2, 4
4 <-> 2, 3, 6
5 <-> 6
6 <-> 4, 5`;

describe('Puzzle', () => {
  describe('Part 1', () => {
    describe('example cases', () => {
      it('example 1', () => {
        expect(part1(example1)).toBe(6);
      });
    });

    it('final input', () => {
      // expect(part1(finalInput)).toBe(134);
    });
  });

  describe('Part 2', () => {
    describe('example cases', () => {
      it('example 1', () => {
        expect(part2(example1)).toBe(2);
      });
    });

    it('final input', () => {
      // expect(part2(finalInput)).toBe(193);
    });
  })
});