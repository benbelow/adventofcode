const part1 = require('./puzzle').part1;
const part2 = require('./puzzle').part2;

const finalInput = require('./input');
const example = `0/2
2/2
2/3
3/4
3/5
0/1
10/1
9/10`;

describe('Puzzle', () => {
  describe('Part 1', () => {
    describe('example cases', () => {
      it('example 1', () => {
        expect(part1(example)).toBe(31);
      });
    });

    it('final input', () => {
      // expect(part1(finalInput)).toBe(1906);
    });
  });

  describe('Part 2', () => {
    describe('example cases', () => {
      it('example 1', () => {
        expect(part2(example)).toBe(19);
      });
    });

    it('final input', () => {
      // expect(part2(finalInput)).toBe(1824);
    });
  })
});