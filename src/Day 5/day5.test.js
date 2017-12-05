const part1 = require('./day5').part1;
const part2 = require('./day5').part2;

const finalInput = require('./input');

describe('Puzzle', () => {
  describe('Part 1', () => {
    describe('example cases', () => {
      it('example 1', () => {
        expect(part1(`0
3
0
1
-3`)).toBe(5);
      });
    });

    it('final input', () => {
      // Long running (~1s)
      // expect(part1(finalInput)).toBe(378980);
    });
  });

  describe('Part 2', () => {
    describe('example cases', () => {
      it('example 1', () => {
        expect(part2(`0
3
0
1
-3`)).toBe(10);
      });
    });

    it('final input', () => {
      // Long running (~70s)
      // expect(part2(finalInput)).toBe(26889114);
    });
  })
});