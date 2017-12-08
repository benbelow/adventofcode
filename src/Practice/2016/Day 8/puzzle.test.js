const part1 = require('./puzzle').part1;
const part2 = require('./puzzle').part2;

const finalInput = require('./input');

describe('Puzzle', () => {
  describe('Part 1', () => {
    describe('example cases', () => {
      it('example 1', () => {
        expect(part1('rect 3x2')).toBe(6);
      });

      it('example 1', () => {
        expect(part1(`rect 3x2
rotate column x=1 by 1
rotate row y=0 by 4
rotate column x=1 by 3`)).toBe(6);
      });
    });

    it('final input', () => {
      expect(part1(finalInput)).toBe(123);
    });
  });
});