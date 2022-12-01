const part1 = require('./puzzle').part1;
const part2 = require('./puzzle').part2;

const finalInput = require('./input');

describe('Puzzle', () => {
  describe('Part 1', () => {
    describe('example cases', () => {
      it('example 1', () => {
        expect(part1('ADVENT')).toBe(6);
      });

      it('example 1', () => {
        expect(part1('A(1x5)BC')).toBe(7);
      });

      it('example 1', () => {
        expect(part1('(3x3)XYZ')).toBe(9);
      });

      it('example 1', () => {
        expect(part1('A(2x2)BCD(2x2)EFG')).toBe(11);
      });

      it('example 1', () => {
        expect(part1('(6x1)(1x3)A')).toBe(6);
      });

      it('example 1', () => {
        expect(part1('A(1x10)BC')).toBe(12);
      });

      it('example 1', () => {
        expect(part1('X(8x2)(3x3)ABCY')).toBe(18);
      });

      it('example 1', () => {
        expect(part1('X(12x1)(3x3)(10x10)ABCY')).toBe(17);
      });
    });

    it('final input', () => {
      // expect(part1(finalInput)).toBe(138735);
    });
  });

  describe('Part 2', () => {
    describe('example cases', () => {
      it('example 1', () => {
        expect(part2('(3x3)XYZ')).toBe(9);
      });

      it('example 1', () => {
        // expect(part2('X(8x2)(3x3)ABCY')).toBe('XABCABCABCABCABCABCY'.length);
      });

      it('example 1', () => {
        expect(part2('(27x12)(20x12)(13x14)(7x10)(1x12)A')).toBe(241920);
      });

      it('example 1', () => {
        expect(part2('(25x3)(3x3)ABC(2x3)XY(5x2)PQRSTX(18x9)(3x2)TWO(5x7)SEVEN')).toBe(445);
      });
    });

    it('final input', () => {
      // expect(part2(finalInput)).toBe(11125026826);
    });
  })
});