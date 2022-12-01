const part1 = require('./puzzle').part1;
const part2 = require('./puzzle').part2;

const finalInput = require('./input');

const example = `Step C must be finished before step A can begin.
Step C must be finished before step F can begin.
Step A must be finished before step B can begin.
Step A must be finished before step D can begin.
Step B must be finished before step E can begin.
Step D must be finished before step E can begin.
Step F must be finished before step E can begin.`;

describe('Puzzle', () => {
    describe('Part 1', () => {
        describe('example cases', () => {
            it('example 1', () => {
                expect(part1(example)).toBe('CABDFE');
            });
        });

        it('final input', () => {
            expect(part1(finalInput)).toBe('BFKEGNOVATIHXYZRMCJDLSUPWQ');
        });
    });

    describe('Part 2', () => {
        describe('example cases', () => {
            it('example 1', () => {
                expect(part2(example, 2, 0)).toBe(15);
            });
        });

        it('final input', () => {
            expect(part2(finalInput)).toBe(1020);
        });
    });
});
