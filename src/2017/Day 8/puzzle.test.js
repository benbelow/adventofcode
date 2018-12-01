const part1 = require('./puzzle').part1;
const part2 = require('./puzzle').part2;

const finalInput = require('./input');

const example1 = `b inc 5 if a > 1
a inc 1 if b < 5
c dec -10 if a >= 1
c inc -20 if c == 10`;

describe('Puzzle', () => {
  describe('Part 1', () => {
    describe('example cases', () => {
      it('example 1', () => {
        expect(part1(example1)).toBe(1);
      });
    });

    it('final input', () => {
      // expect(part1(finalInput)).toBe(3880);
    });
  });

  describe('Part 2', () => {
    describe('example cases', () => {
      it('example 1', () => {
        expect(part2(example1)).toBe(10);
      });
    });

    it('final input', () => {
      // expect(part2(finalInput)).toBe(5035);
    });
  })
});