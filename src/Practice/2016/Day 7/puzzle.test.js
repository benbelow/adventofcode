const part1 = require('./puzzle').part1;
const part2 = require('./puzzle').part2;
const tls = require('./puzzle').IPsupportsTLS;

const finalInput = require('./input');

describe('Puzzle', () => {
  describe('Part 1', () => {
    describe('example cases', () => {
      it('example 1', () => {
        expect(tls('abba[mnop]qrst')).toBe(true);
      });

      it('example 1', () => {
        expect(tls('abcd[bddb]xyyx')).toBe(false);
      });

      it('example 1', () => {
        expect(tls('aaaa[qwer]tyui')).toBe(false);
      });

      it('example 1', () => {
        expect(tls('ioxxoj[asdfgh]zxcvbn')).toBe(true);
      });

      it('example 1', () => {
        expect(tls('one[asdfgh]two[hhdhjd]threer')).toBe(true);
      });

      it('example 1', () => {
        expect(tls('ioxxoj[asdfgh]zxcvbn[poop]')).toBe(false);
      });
    });

    it('final input', () => {
      // expect(part1(finalInput)).toBe(105);
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