const part1 = require('./puzzle').part1;
const part2 = require('./puzzle').part2;
const tls = require('./puzzle').IPsupportsTLS;
const ssl = require('./puzzle').IPsupportsSSL;

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
        expect(ssl('aba[bab]xyz')).toBe(true);
      });

      it('example 1', () => {
        expect(ssl('xyx[xyx]xyx')).toBe(false);
      });

      it('example 1', () => {
        expect(ssl('aaa[kek]eke')).toBe(true);
      });

      it('example 1', () => {
        expect(ssl('zazbz[bzb]cdb')).toBe(true);
      });

      it('example 1', () => {
        expect(ssl('za[bzb]cdb[jsjs]zbz')).toBe(true);
      });

      it('example 1', () => {
        expect(ssl('zaz[999]cdb[aza]zvzbzbzbzkbzb')).toBe(true);
      });

      it('example 1', () => {
        expect(ssl('za[bzb]cdb[jsjs]bzp')).toBe(false);
      });

      it('example 1', () => {
        expect(ssl('fcrwgutcgcqizev[nwszwhfvqtdhrymgqf]iiahiososrpdafnt[gbkrardsossgcvu]fmudukrxbiqyrpi')).toBe(true);
      });
    });

    it('final input', () => {
      // expect(part2(finalInput)).toBe(258);
    });
  })
});