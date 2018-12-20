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
                expect(part1(example)).toEqual([7, 3]);
            });
        });

        it('final input', () => {
            expect(part1(finalInput)).toEqual('');
        });
    });

    describe('Part 2', () => {
        describe('example cases', () => {
            it('example 1', () => {
                // expect(part2(0)).toBe(0);
            });
        });

        it('final input', () => {
            // expect(part2(finalInput)).toBe('');
        });
    });
});
