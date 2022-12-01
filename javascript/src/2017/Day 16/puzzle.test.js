const part1 = require('./puzzle').part1;
const part2 = require('./puzzle').part2;

const finalInput = require('./input');

const example = 's1, x3/4, pe/b';

describe('Puzzle', () => {
  describe('Part 1', () => {
    describe('example cases', () => {
      it('example 1', () => {
        expect(part1(example, 5)).toBe('baedc');
      });
    });

    it('final input', () => {
      // expect(part1(finalInput)).toBe('ociedpjbmfnkhlga');
    });
  });

  describe('Part 2', () => {
    it('final input', () => {
      // expect(part2(finalInput)).toBe('gnflbkojhicpmead');
    });
  })
});