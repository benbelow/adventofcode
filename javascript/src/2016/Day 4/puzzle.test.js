const part1 = require('./puzzle').part1;
const part2 = require('./puzzle').part2;
const isRealRoom = require('./puzzle').isRealRoom;
const unencryptRoom = require('./puzzle').unencryptRoom;

const finalInput = require('./input');

describe('Puzzle', () => {
  describe('Part 1', () => {
    describe('example cases', () => {
      describe('isRealRoom', function () {
        it('1', function () {
          expect(isRealRoom('aaaaa-bbb-z-y-x-123[abxyz]')).toBe(true);
        });
        it('1', function () {
          expect(isRealRoom('a-b-c-d-e-f-g-h-987[abcde]')).toBe(true);
        });
        it('1', function () {
          expect(isRealRoom('not-a-real-room-404[oarel]')).toBe(true);
        });
        it('1', function () {
          expect(isRealRoom('totally-real-room-200[decoy]')).toBe(false);
        });
      });

      it('example 1', () => {
        const input = `aaaaa-bbb-z-y-x-123[abxyz]
a-b-c-d-e-f-g-h-987[abcde]
not-a-real-room-404[oarel]
totally-real-room-200[decoy]`;
        expect(part1(input)).toBe(1514);
      });
    });

    it('final input', () => {
      // expect(part1(finalInput)).toBe(245102);
    });
  });

  describe('Part 2', () => {
    describe('example cases', () => {
      it('unencrypt', () => {
        expect(unencryptRoom('qzmt-zixmtkozy-ivhz-343')).toBe('very encrypted name');
      });
    });

    it('final input', () => {
      // expect(part2(finalInput)).toBe('324');
    });
  })
});