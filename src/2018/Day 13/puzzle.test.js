const part1 = require('./puzzle').part1;
const part2 = require('./puzzle').part2;

const example = `/->-\\        
|   |  /----\\
| /-+--+-\\  |
| | |  | v  |
\\-+-/  \\-+--/
  \\------/ `;

const finalInput = require('./input');

describe('Puzzle', () => {
    describe('Part 1', () => {
        describe('example cases', () => {
            it('example 1', () => {
                // expect(part1(example).answer).toEqual([7, 3]);
            });
        });

        it('final input', () => {
            // expect(part1(finalInput).answer).toEqual([39, 52]);
        });
    });

    const example2 = `/>-<\\  
|   |  
| /<+-\\
| | | v
\\>+</ |
  |   ^
  \\<->/`;

    describe('Part 2', () => {
        describe('example cases', () => {
            it('example 1', () => {
                // expect(part1(example2).coord).toEqual([6,4]);
            });
        });

        it('final input', () => {
            // takes 25 minutes :P
            // expect(part1(finalInput).coord).toEqual([]);
        });
    });
});
