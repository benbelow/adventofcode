const part1 = require('./puzzle').part1;
const part2 = require('./puzzle').part2;
const isMatch = require('./puzzle').isMatch;

const finalInput = require('./input');

const example = `dabAcCaCBAcCcaDA`;

describe('Puzzle', () => {
  describe('isMatch', () => {
    it('works', () => {
      expect(isMatch('a', 'A')).toBe(true);
      expect(isMatch('A', 'a')).toBe(true);
      expect(isMatch('a', 'a')).toBe(false);
      expect(isMatch('A', 'A')).toBe(false);
      expect(isMatch('b', 'B')).toBe(true);
      expect(isMatch('b', 'A')).toBe(false);
    });
  });

  describe('Part 1', () => {
    describe('example cases', () => {
      it('example 1', () => {
        expect(part1(example)).toBe(10);
      });
    });

    it('final input', () => {
      expect(part1(finalInput)).toBe('');
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