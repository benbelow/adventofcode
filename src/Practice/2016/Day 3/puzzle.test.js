const part1 = require('./puzzle').part1;
const part2 = require('./puzzle').part2;
const isValidTriangle = require('./puzzle').isValidTriangle;

const finalInput = require('./input');

describe('Puzzle', () => {
  describe('Part 1', () => {
    describe('example cases', () => {
      it('example 1', () => {
        expect(isValidTriangle(5, 10, 25)).toBe(false);
      });

      it('example 1', () => {
        expect(isValidTriangle(5, 10, 12)).toBe(true);
      });

      it('example 1', () => {
        expect(isValidTriangle(566, 477, 376)).toBe(true);
      });

      it('example 1', () => {
        expect(part1('566 477 376')).toBe(1);
      });
    });

    it('final input', () => {
      expect(part1(finalInput)).toBe(1050);
    });
  });

  describe('Part 2', () => {
    describe('example cases', () => {
      it('example 1', () => {
        expect(part2(`566 5 5
477 10 10
376 25 25
566 5 5
477 10 10
376 25 25`)).toBe(2);
      });
    });

    it('final input', () => {
      expect(part2(finalInput)).toBe(1921);
    });
  })
});