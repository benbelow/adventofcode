const part1 = require('./puzzle').part1;
const part2 = require('./puzzle').part2;

const finalInput = require('./input');

describe('Puzzle', () => {
  describe('Part 1', () => {
    describe('example cases', () => {
      it('example 1', () => {
        // expect(part1('abc')).toBe('18f47a30');
      });
    });

    it('final input', () => {
      // expect(part1(finalInput)).toBe('f97c354d');
    });
  });

  describe('Part 2', () => {
    describe('example cases', () => {
      it('example 1', () => {
        // expect(part2('abc')).toBe('05ace8e3');
      });
    });

    it('final input', () => {
      // expect(part2(finalInput)).toBe('');
    });
  })
});