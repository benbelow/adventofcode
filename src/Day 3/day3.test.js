const spiral = require('./day3').spiral;
const lowestSquareRootHigherThan = require('./day3').lowestSquareRootHigherThan;
const highestSquareRootLowerThan = require('./day3').highestSquareRootLowerThan;
const distanceToCenterOfRing = require('./day3').distanceToCenterOfRing;
const firstLargerValueThan = require('./day3').firstLargerValueThan;

describe('Day 3', () => {
  describe('Part 1', () => {
    describe('example test inputs', () => {
      it('example 1', () => {
        expect(spiral(1)).toBe(0);
      })

      it('example 2', () => {
        expect(spiral(12)).toBe(3);
      })

      it('example 3', () => {
        expect(spiral(23)).toBe(2);
      })

      it('example 1', () => {
        expect(spiral(1024)).toBe(31);
      })
    })

    describe('final test', () => {
      expect(spiral(368078)).toBe(371);
    })

    describe('lowestSquareHigherThan', () => {
      it('1', () => {
        expect(lowestSquareRootHigherThan(1)).toBe(1);
      })

      it('8', () => {
        expect(lowestSquareRootHigherThan(8)).toBe(3);
      })

      it('9', () => {
        expect(lowestSquareRootHigherThan(9)).toBe(3);
      })
    })

    describe('highestSquareRootLowerThan', () => {
      it('1', () => {
        expect(highestSquareRootLowerThan(1)).toBe(1);
      })

      it('8', () => {
        expect(highestSquareRootLowerThan(8)).toBe(3);
      })

      it('9', () => {
        expect(highestSquareRootLowerThan(9)).toBe(3);
      })

      it('30', () => {
        expect(highestSquareRootLowerThan(30)).toBe(7);
      })
    })

    describe('distanceToCenterOfRing', () => {
      it('test 1', () => {
        expect(distanceToCenterOfRing(3)).toBe(1);
      })

      it('test 2', () => {
        expect(distanceToCenterOfRing(17)).toBe(2);
      })

      it('test 3', () => {
        expect(distanceToCenterOfRing(22)).toBe(1);
      })

      it('test 4', () => {
        expect(distanceToCenterOfRing(15)).toBe(0);
      })

      it('test 5', () => {
        expect(distanceToCenterOfRing(25)).toBe(2);
      })
    })
  })

  describe('Part 2', () => {
    describe('example inputs', () => {
      it('example 1', () => {
        expect(firstLargerValueThan(0)).toBe(1);
      })

      it('example 2', () => {
        expect(firstLargerValueThan(5)).toBe(10);
      })

      it('example 1', () => {
        expect(firstLargerValueThan(25)).toBe(26);
      })

      it('example 1', () => {
        expect(firstLargerValueThan(747)).toBe(806);
      })
    })
  })

})
