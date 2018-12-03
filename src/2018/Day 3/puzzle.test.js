const part1 = require('./puzzle').part1;
const part2 = require('./puzzle').part2;

const finalInput = require('./input');

let example = `#1 @ 1,3: 4x4
#2 @ 3,1: 4x4
#3 @ 5,5: 2x2`;

describe('Puzzle', () => {
    describe('Part 1', () => {
        describe('example cases', () => {
            it('example 1', () => {
                expect(part1(example)).toBe(4);
            });
        });

        it('final input', () => {
            // SLOW!
            // expect(part1(finalInput)).toBe(96569);
        });
    });

    describe('Part 2', () => {
        describe('example cases', () => {
            it('example 1', () => {
                expect(part2(0)).toBe(3);
            });
        });

        it('final input', () => {
            // expect(part2(finalInput)).toBe('');
        });
    });
});
