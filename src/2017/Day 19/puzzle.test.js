const part1 = require('./puzzle').part1;
const part2 = require('./puzzle').part2;

const finalInput = require('./input');
const example = `     |          
     |  +--+    
     A  |  C    
 F---|----E|--+ 
     |  |  |  D 
     +B-+  +--+ `;

describe('Puzzle', () => {
  describe('Part 1', () => {
    describe('example cases', () => {
      it('example 1', () => {
        // expect(part1(example)).toBe('ABCDEF');
      });
    });

    it('final input', () => {
      // expect(part1(finalInput)).toBe('LIWQYKMRP');
    });
  });

  describe('Part 2', () => {
    describe('example cases', () => {
      it('example 1', () => {
        // expect(part2(example)).toBe(38);
      });
    });

    it('final input', () => {
      // expect(part2(finalInput)).toBe(16764);
    });
  })
});