const part1 = require('./puzzle').part1;
const part2 = require('./puzzle').part2;

const finalInput = require('./input');

describe('Puzzle', () => {
  describe('Part 1', () => {
    describe('example cases', () => {
      it('example 1', () => {
        // expect(part1('abba[mnop]qrst')).toBe(true);
      });

      it('example 1', () => {
        // expect(part1('abcd[bddb]xyyx')).toBe(false);
      });

      it('example 1', () => {
        // expect(part1('aaaa[qwer]tyui')).toBe(false);
      });

      it('example 1', () => {
        // expect(part1('ioxxoj[asdfgh]zxcvbn')).toBe(true);
      });
    });

    it('final input', () => {
      // expect(part1(finalInput)).toBe('');
    });
  });

  describe('Part 2', () => {
    describe('example cases', () => {
      it('example 1', () => {
        expect(part2(0)).toBe(0);
      });
    });

    it('final input', () => {
      // expect(part2(finalInput)).toBe('');
    });
  })
});