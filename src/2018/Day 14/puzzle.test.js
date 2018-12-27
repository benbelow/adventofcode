const part1 = require('./puzzle').part1;
const part2 = require('./puzzle').part2;

const finalInput = require('./input');

describe('Puzzle', () => {
  describe('Part 1', () => {
    describe('example cases', () => {
      it('example 1', () => {
        expect(part1(9)).toBe('5158916779');
      });
      it('example 2', () => {
        expect(part1(5)).toBe('0124515891');
      });
      it('example 1', () => {
        expect(part1(18)).toBe('9251071085');
      });
      it('example 1', () => {
        expect(part1(2018)).toBe('5941429882');
      });
    });

    it('final input', () => {
      expect(part1(509671)).toBe('');
    });
  });

  describe('Part 2', () => {
    describe('example cases', () => {
      it('example 1', () => {
        // expect(part2(0)).toBe(0);
      });
    });

    it('final input', () => {
      // expect(part2(finalInput)).toBe('');
    });
  })
});
