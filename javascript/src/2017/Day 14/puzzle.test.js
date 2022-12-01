const part1 = require('./puzzle').part1;
const cachedPart1 = require('./puzzle').cachedPart1;
const part2 = require('./puzzle').part2;

const finalInput = require('./input').input;

const cachedExample = require('./input').cachedExample;
const cachedFinal = require('./input').cachedFinal;

describe('Puzzle', () => {
  describe('Part 1', () => {
    describe('example cases', () => {
      it('example 1', () => {
        // expect(part1('flqrgnkx')).toBe(8108);
      });
    });

    it('final input', () => {
      // expect(part1(finalInput)).toBe(8074);
    });
  });

  describe('Part 2', () => {
    describe('example cases', () => {
      it('caching', () => {
        // expect(cachedPart1(cachedExample)).toBe(8108);
      });

      it('caching', () => {
        // expect(cachedPart1(cachedFinal)).toBe(8074);
      });

      it('blocks', () => {
        expect(part2(`[
'11010100',
'11010101']`)).toBe(4);
      });

      it('example 1', () => {
        // expect(part2(cachedExample)).toBe(1242);
      });
    });

    it('final input', () => {
      // expect(part2(cachedFinal)).toBe(1212);
    });
  })
});
