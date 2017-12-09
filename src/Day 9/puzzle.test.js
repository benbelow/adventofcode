const part1 = require('./puzzle').part1;
const part2 = require('./puzzle').part2;

const finalInput = require('./input');

describe('Puzzle', () => {
  describe('Part 1', () => {
    describe('example cases', () => {
      it('example 1', () => {
        expect(part1('{}')).toBe(1);
      });

      it('example 1', () => {
        expect(part1('{{{}}}')).toBe(6);
      });

      it('example 1', () => {
        expect(part1('{{},{}}')).toBe(5);
      });

      it('example 1', () => {
        expect(part1('{{{},{},{{}}}}')).toBe(16);
      });

      it('example 1', () => {
        expect(part1('{<a>,<a>,<a>,<a>}')).toBe(1);
      });

      it('example 1', () => {
        expect(part1('{{<ab>},{<ab>},{<ab>},{<ab>}}')).toBe(9);
      });

      it('example 1', () => {
        expect(part1('{{<!!>},{<!!>},{<!!>},{<!!>}}')).toBe(9);
      });

      it('example 1', () => {
        expect(part1('{{<a!>},{<a!>},{<a!>},{<ab>}}')).toBe(3);
      });

      it('example 1', () => {
        expect(part1('{<{o"i!a,<{i<a>}')).toBe(1);
      });

    });

    it('final input', () => {
      // expect(part1(finalInput)).toBe(10820);
    });
  });

  describe('Part 2', () => {
    describe('example cases', () => {
      it('example 1', () => {
        expect(part2('<>')).toBe(0);
      });

      it('example 1', () => {
        expect(part2('<random characters>')).toBe(17);
      });

      it('example 1', () => {
        expect(part2('<<<<>')).toBe(3);
      });

      it('example 1', () => {
        expect(part2('<{!>}>')).toBe(2);
      });

      it('example 1', () => {
        expect(part2('<!!>')).toBe(0);
      });

      it('example 1', () => {
        expect(part2('<{o"i!a,<{i<a>')).toBe(10);
      });

      it('example 1', () => {
        expect(part2('<!!!>>')).toBe(0);
      });

    });

    it('final input', () => {
      // expect(part2(finalInput)).toBe(5547);
    });
  })
});