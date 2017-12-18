const part1 = require('./puzzle').part1;
const part2 = require('./puzzle').part2;

const finalInput = require('./input');
const example =`set a 1
add a 2
mul a a
mod a 5
snd a
set a 0
rcv a
jgz a -1
set a 1
jgz a -2`;

describe('Puzzle', () => {
  describe('Part 1', () => {
    describe('example cases', () => {
      it('example 1', () => {
        // expect(part1(example)).toBe(4);
      });
    });

    it('final input', () => {
      // expect(part1(finalInput)).toBe(3188);
    });
  });

  describe('Part 2', () => {
    describe('example cases', () => {
      it('example 1', () => {
        expect(part2(`snd 1
snd 2
snd p
rcv a
rcv b
rcv c
rcv d`)).toBe(3);
      });
    });

    it('final input', () => {
      expect(part2(finalInput)).toBe('');
    });
  })
});