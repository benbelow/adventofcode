const part1 = require('./day1').part1;
const part2 = require('./day1').part2;

const finalInput = 'L1, L3, L5, L3, R1, L4, L5, R1, R3, L5, R1, L3, L2, L3, R2, R2, L3, L3, R1, L2, R1, L3, L2, R4, R2, L5, R4, L5, R4, L2, R3, L2, R4, R1, L5, L4, R1, L2, R3, R1, R2, L4, R1, L2, R3, L2, L3, R5, L192, R4, L5, R4, L1, R4, L4, R2, L5, R45, L2, L5, R4, R5, L3, R5, R77, R2, R5, L5, R1, R4, L4, L4, R2, L4, L1, R191, R1, L1, L2, L2, L4, L3, R1, L3, R1, R5, R3, L1, L4, L2, L3, L1, L1, R5, L4, R1, L3, R1, L2, R1, R4, R5, L4, L2, R4, R5, L1, L2, R3, L4, R2, R2, R3, L2, L3, L5, R3, R1, L4, L3, R4, R2, R2, R2, R1, L4, R4, R1, R2, R1, L2, L2, R4, L1, L2, R3, L3, L5, L4, R4, L3, L1, L5, L3, L5, R5, L5, L4, L2, R1, L2, L4, L2, L4, L1, R4, R4, R5, R1, L4, R2, L4, L2, L4, R2, L4, L1, L2, R1, R4, R3, R2, R2, R5, L1, L2';

describe('Day1', () => {
  describe('Part 1', () => {
    describe('example cases', () => {
      it('example 1', () => {
        expect(part1('R2, L3')).toBe(5);
      });

      it('example 2', () => {
        expect(part1('R2, R2, R2')).toBe(2);
      });

      it('example 3', () => {
        expect(part1('R5, L5, R5, R3')).toBe(12);
      });

      it('example 4', () => {
        expect(part1('R15')).toBe(15);
      });
    });

    it('final input', () => {
      expect(part1(finalInput))
        .toBe(299);
    });
  });

  describe('Part 2', () => {
    describe('example cases', () => {
      it('example 1', () => {
        expect(part2('R8, R4, R4, R8')).toBe(4);
      });
    });

    it('final input', () => {
      // expect(part2(finalInput)).toBe(181);
    });
  })
});