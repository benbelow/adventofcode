const part1 = require('./puzzle').part1;
const part2 = require('./puzzle').part2;

const finalInput = require('./input');

describe('Puzzle', () => {
  describe('Part 1', () => {
    describe('example cases', () => {
      it('example 1', () => {
        expect(part1(3)).toBe(638);
      });
    });

    it('final input', () => {
      expect(part1(finalInput)).toBe(1642);
    });
  });

  describe('Part 2', () => {
    describe('example cases', () => {
      it('example 1', () => {
        expect(part2(3, 2017)).toBe(1226);
      });
    });

    it('final input', () => {
      // expect(part2(finalInput)).toBe(33601318);
    });
  })
});