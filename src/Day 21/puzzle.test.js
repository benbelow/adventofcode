const part1 = require('./puzzle').part1;
const part2 = require('./puzzle').part2;

const finalInput = require('./input');
const example = `../.# => ##./#../...
.#./..#/### => #..#/..../..../#..#`;

describe('Puzzle', () => {
  describe('Part 1', () => {
    describe('example cases', () => {
      it('example 1', () => {
        expect(part1(example, 2)).toBe(12);
      });
    });

    it('final input', () => {
      // expect(part1(finalInput)).toBe(190);
    });
  });

  describe('Part 2', () => {
    describe('example cases', () => {
      it('example 1', () => {
        // expect(part2(0)).toBe(0);
      });
    });

    it('final input', () => {
      // expect(part1(finalInput, 18)).toBe(2335049);
    });
  })
});