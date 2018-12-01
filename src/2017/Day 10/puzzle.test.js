const part1 = require('./puzzle').part1;
const part2 = require('./puzzle').part2;

const finalInput = require('./input');

const example1 = `3, 4, 1, 5`;

describe('Puzzle', () => {
  describe('Part 1', () => {
    describe('example cases', () => {
      it('example 1', () => {
        expect(part1(example1, 5)).toBe(12);
      });
    });

    it('final input', () => {
      expect(part1(finalInput)).toBe(46600);
    });
  });

  describe('Part 2', () => {
    describe('example cases', () => {
      it('example 1', () => {
        // expect(part2('')).toBe('a2582a3a0e66e6e86e3812dcb672a272');
      });

      it('example 1', () => {
        // expect(part2('AoC 2017')).toBe('33efeb34ea91902bb2f59c9920caa6cd');
      });

      it('example 1', () => {
        // expect(part2('1,2,3')).toBe('3efbe78a8d82f29979031a4aa0b16a9d');
      });

      it('example 1', () => {
        // expect(part2('1,2,4')).toBe('63960835bcdc130f0b66d7ff4f6a5a8e');
      });

    });

    it('final input', () => {
      // expect(part2(finalInput)).toBe('23234babdc6afa036749cfa9b597de1b');
    });
  })
});