const part1 = require('./puzzle').part1;
const part2 = require('./puzzle').part2;

const finalInput = require('./input');

const example = `1, 1
1, 6
8, 3
3, 4
5, 5
8, 9`;

describe('Puzzle', () => {
    describe('Part 1', () => {
        describe('example cases', () => {
            it('example 1', () => {
                expect(part1(example)).toBe(17);
            });
        });

        it('final input', () => {
            // expect(part1(finalInput)).toBe(3238);
        });
    });

    describe('Part 2', () => {
        describe('example cases', () => {
            it('example 1', () => {
                expect(part2(example, 32)).toBe(16);
            });
        });

        it('final input', () => {
            // expect(part2(finalInput)).toBe(45046);
        });
    });
});
