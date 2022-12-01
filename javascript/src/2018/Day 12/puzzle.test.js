const part1 = require('./puzzle').part1;
const part2 = require('./puzzle').part2;

const finalInput = require('./input');

const example = `initial state: #..#.#..##......###...###

...## => #
..#.. => #
.#... => #
.#.#. => #
.#.## => #
.##.. => #
.#### => #
#.#.# => #
#.### => #
##.#. => #
##.## => #
###.. => #
###.# => #
####. => #`;

describe('Puzzle', () => {
    describe('Part 1', () => {
        describe('example cases', () => {
            it('example 1', () => {
                expect(part1(example)).toBe(325);
            });
        });

        it('final input', () => {
            expect(part1(finalInput)).toBe(4110);
        });
    });

    const example2 = `initial state: ......

...## => #
..#.. => #
.#... => #
.#.#. => #
.#.## => #
.##.. => #
.#### => #
#.#.# => #
#.### => #
##.#. => #
##.## => #
###.. => #
###.# => #
####. => #`;

    describe('Part 2', () => {
        describe('example cases', () => {
            it('example 1', () => {
                expect(part1(example2, 600000)).toBe(0);
            });
        });

        it('final input', () => {
            // console.log(part1(finalInput, 5000, console.log));
            //
            for(let x = 0; x < 10000; x += 1000) {
                console.log(x, part1(finalInput, x));
            }
            // actually off by one error - should add 53
            console.log(11066 + (53 * (50000000000 - 199)))
            console.log(53519 + ((53000 * (50000000000 - 1000)) / 1000))
            expect(part1(finalInput, 50000000000)).toBe(2650000000519);
        });
    });
});
