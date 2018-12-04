const part1 = require('./puzzle').part1;
const part2 = require('./puzzle').part2;

const finalInput = require('./input');

const example = `[1518-11-01 00:00] Guard #10 begins shift
[1518-11-01 00:05] falls asleep
[1518-11-01 00:25] wakes up
[1518-11-01 00:30] falls asleep
[1518-11-01 00:55] wakes up
[1518-11-03 00:05] Guard #10 begins shift
[1518-11-02 00:40] falls asleep
[1518-11-02 00:50] wakes up
[1518-11-03 00:24] falls asleep
[1518-11-04 00:36] falls asleep
[1518-11-04 00:02] Guard #99 begins shift
[1518-11-01 23:58] Guard #99 begins shift
[1518-11-03 00:29] wakes up
[1518-11-04 00:46] wakes up
[1518-11-05 00:03] Guard #99 begins shift
[1518-11-05 00:45] falls asleep
[1518-11-05 00:55] wakes up`;

describe('Puzzle', () => {
  describe('Part 1', () => {
    describe('example cases', () => {
      it('example 1', () => {
        expect(part1(example)).toBe(240);
      });
    });

    it('final input', () => {
      expect(part1(finalInput)).toBe(50558);
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
  })
});
