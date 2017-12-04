const part1 = require('./day5').part1;
const part2 = require('./day5').part2;

const finalInput = '';

describe('Day 5', () => {
  describe('Part 1', () => {
    describe('example cases', () => {
      it('example 1', () => {
        expect(part1(0)).toBe(0);
      });
    });

    it('final input', () => {
      expect(part1(finalInput)).toBe('');
    });
  });

  describe('Part 2', () => {
    describe('example cases', () => {
      it('example 1', () => {
        expect(part2(0)).toBe(0);
      });
    });

    it('final input', () => {
      expect(part2(finalInput)).toBe('');
    });
  })
});