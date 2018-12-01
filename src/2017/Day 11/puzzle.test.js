const part1 = require('./puzzle').part1;
const part2 = require('./puzzle').part2;

const finalInput = require('./input');

describe('Puzzle', () => {
  describe('Part 1', () => {
    describe('example cases', () => {
      it('example 1', () => {
        expect(part1('ne,ne,ne')).toBe(3);
      });

      it('example 1', () => {
        expect(part1('ne,ne,sw,sw')).toBe(0);
      });


      it('example 1', () => {
        expect(part1('ne,ne,s,s')).toBe(2);
      });


      it('example 1', () => {
        expect(part1('se,sw,se,sw,sw')).toBe(3);
      });

    });

    it('final input', () => {
      // expect(part1(finalInput)).toBe(675);
    });
  });

  describe('Part 2', () => {

    it('final input', () => {
      // expect(part2(finalInput)).toBe(1424);
    });
  })
});