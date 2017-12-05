const part1 = require('./puzzle').part1;
const part2 = require('./puzzle').part2;

const finalInput = require('./input');

describe('Puzzle', () => {
  describe('Part 1', () => {
    const example = `eedadn
drvtee
eandsr
raavrd
atevrs
tsrnev
sdttsa
rasrtv
nssdts
ntnada
svetve
tesnvt
vntsnd
vrdear
dvrsen
enarar`;

    describe('example cases', () => {
      it('example 1', () => {
        expect(part1(example)).toBe('easter');
      });
    });

    it('final input', () => {
      // expect(part1(finalInput)).toBe('usccerug');
    });
  });

  describe('Part 2', () => {
    describe('example cases', () => {
      it('example 1', () => {
        expect(part2(0)).toBe(0);
      });
    });

    it('final input', () => {
      expect(part2(finalInput)).toBe('');
    });
  })
});