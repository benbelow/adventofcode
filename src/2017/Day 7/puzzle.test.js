const part1 = require('./puzzle').part1;
const part2 = require('./puzzle').part2;

const finalInput = require('./input');

const part1Example1 = `pbga (66)
xhth (57)
ebii (61)
havc (66)
ktlj (57)
fwft (72) -> ktlj, cntj, xhth
qoyq (66)
padx (45) -> pbga, havc, qoyq
tknk (41) -> ugml, padx, fwft
jptl (61)
ugml (68) -> gyxo, ebii, jptl
gyxo (61)
cntj (57)`;

describe('Puzzle', () => {
  describe('Part 1', () => {
    describe('example cases', () => {
      it('example 1', () => {
        expect(part1(part1Example1)).toBe('tknk');
      });
    });

    it('final input', () => {
      // expect(part1(finalInput)).toBe('eqgvf');
    });
  });

  describe('Part 2', () => {
    describe('example cases', () => {
      it('example 1', () => {
        expect(part2(part1Example1)).toBe(60);
      });
    });

    it('final input', () => {
      // expect(part2(finalInput)).toBe(757);
    });
  })
});