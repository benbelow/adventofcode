const part1 = require('./puzzle').part1;
const part2 = require('./puzzle').part2;

const finalInput = require('./input');

const example1 = `..#
#..
...`;

describe('Puzzle', () => {
  describe('Part 1', () => {
    describe('example cases', () => {
      it('example 1', () => {
        // expect(part1(example1)).toBe(5587);
      });

      it('example 1', () => {
        expect(part1(example1, 70)).toBe(41);
      });

      it('example 1', () => {
        expect(part1(example1, 7)).toBe(5);
      });
    });

    it('final input', () => {
      // expect(part1(finalInput)).toBe(5352);
    });
  });

  describe('Part 2', () => {
    describe('example cases', () => {
      it('example 1', () => {
        expect(part2(example1,  100)).toBe(26);
      });
    });

    it('final input', () => {
      // expect(part2(finalInput)).toBe(2511475);
    });
  })
});