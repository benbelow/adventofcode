const part1 = require('./puzzle').part1;
const part2 = require('./puzzle').part2;

const finalInput = require('./input');
const example1 = ``;

describe('Puzzle', () => {
  describe('Part 1', () => {
    it('final input', () => {
      // expect(part1(finalInput)).toBe(9409);
    });
  });

  describe('Part 2', () => {
    describe('example cases', () => {
      it('final input 3', () => {
        expect(part2(finalInput)).toBe(913);
      });
    })
  })
});