const part1 = require('./day6').part1;
const part2 = require('./day6').part2;

const finalInput = require('./input');

const example = ``;

describe('Puzzle', () => {
  describe('Part 1', () => {
    describe('example cases', () => {
      it('example 1', () => {
        const input = `0, 2, 7, 0`;

        expect(part1(input)).toBe(5);
      });
    });

    it('final input', () => {
      // expect(part1(finalInput)).toBe(3156);
    });
  });

  describe('Part 2', () => {
    describe('example cases', () => {
      it('example 1', () => {
        const input = ``;

        expect(part2(0)).toBe(0);
      });
    });

    it('final input', () => {
      // expect(part2(finalInput)).toBe('');
    });
  })
});