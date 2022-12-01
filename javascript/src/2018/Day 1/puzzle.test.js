const part1 = require('./puzzle').part1;
const part2 = require('./puzzle').part2;

const finalInput = require('./input');

describe('Puzzle', () => {
  describe('Part 1', () => {
    describe('example cases', () => {
      it('example 1', () => {
        expect(part1('+1, -2, +3, +1')).toBe(3);
      });
    });

    it('final input', () => {
      expect(part1(finalInput)).toBe(439);
    });
  });

  describe('Part 2', () => {
    describe('example cases', () => {
      it('example 1', () => {
        expect(part2('+1, -1')).toBe(0);
      });
      it('example 2', () => {
        expect(part2('+3, +3, +4, -2, -4')).toBe(10);
      });
      it('example 3', () => {
        expect(part2('-6, +3, +8, +5, -6')).toBe(5);
      });
      it('example 4', () => {
        expect(part2('+7, +7, -2, -7, -4')).toBe(14);
      });
    });

    it('final input', () => {
      // expect(part2(finalInput)).toBe(124645);
    });
  })
});