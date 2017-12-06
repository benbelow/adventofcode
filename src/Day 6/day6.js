const _ = require('lodash');

Array.prototype.compare = function(testArr) {
  if (this.length != testArr.length) return false;
  for (var i = 0; i < testArr.length; i++) {
    if (this[i].compare) { //To test values in nested arrays
      if (!this[i].compare(testArr[i])) return false;
    }
    else if (this[i] !== testArr[i]) return false;
  }
  return true;
};

const part1 = (input) => {
  const numbers = _.map(input.split(/ |\t+/), i => parseInt(i));
  let i = 0;

  console.log(numbers);

  const seenStates = [_.clone(numbers)];

  while ((_.uniqWith(seenStates, (x, y) => x.compare(y)).length === seenStates.length) && i >= 0) {
    const highestIndex = _.reduce(numbers, (s, n, i) => {
      if (n > numbers[s]) {
        return i;
      } else {
        return s;
      }
    }, 0);
    const highest = Object.assign({}, {value: numbers[highestIndex] });

    let currentIndex = highestIndex;
    numbers[highestIndex] = 0;
    for(let j = 1; j <= highest.value; j+=1) {
      currentIndex = (currentIndex + 1) % numbers.length;
      numbers[currentIndex] += 1;
    }

    seenStates.push(_.clone(numbers));
    i++;
  }
  return seenStates.length - 1;
};

const part2 = (input) => {
  const numbers = _.map(input.split(/ |\t+/), i => parseInt(i));
  let i = 0;

  console.log(numbers);

  const seenStates = [_.clone(numbers)];

  while ((_.uniqWith(seenStates, (x, y) => x.compare(y)).length === seenStates.length)) {
    const highestIndex = _.reduce(numbers, (s, n, i) => {
      if (n > numbers[s]) {
        return i;
      } else {
        return s;
      }
    }, 0);
    const highest = Object.assign({}, {value: numbers[highestIndex] });

    let currentIndex = highestIndex;
    numbers[highestIndex] = 0;
    for(let j = 1; j <= highest.value; j+=1) {
      currentIndex = (currentIndex + 1) % numbers.length;
      numbers[currentIndex] += 1;
    }

    seenStates.push(_.clone(numbers));
    i++;
  }

  let firstIndex;
  _.each(seenStates, (s, i) => {
    if(s.compare(_.last(seenStates)) && !firstIndex) {
      firstIndex = i;
      console.log(firstIndex);
    }
  });

  return seenStates.length -1 - firstIndex;
};

module.exports = {
  part1,
  part2,
};