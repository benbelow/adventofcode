const _ = require('lodash');

const alphabet = 'abcdefghijklmnopqrstuvwxyz';

String.prototype.replaceAt=function(index, replacement) {
  return this.substr(0, index) + replacement+ this.substr(index + replacement.length);
};

const part1 = (input, numDancers = 16) => {
  let dancers = alphabet.slice(0, numDancers);

  const instructions = input.split(',').map(x => x.trim());

  const spin = x => dancers = _.reduce(dancers, (res, d, i) => (res + dancers[(i - parseInt(x) + dancers.length) % dancers.length]), '');

  const exchange = (x, y) => {
    const xInt = parseInt(x);
    const yInt = parseInt(y);
    const pivot = dancers[xInt];
    dancers = dancers.replaceAt(xInt, dancers[yInt]);
    dancers = dancers.replaceAt(yInt, pivot);
  };

  const partner = (x, y) => {
    const xInt = dancers.indexOf(x);
    const yInt = dancers.indexOf(y);
    const pivot = dancers[xInt];
    dancers = dancers.replaceAt(xInt, dancers[yInt]);
    dancers = dancers.replaceAt(yInt, pivot);
  };

  const instructionType = {'s': spin, 'x': exchange, 'p': partner};

  _.each(instructions, i => {
    instructionType[i[0]](i.slice(1, i.length).split('/')[0], i.slice(1, i.length).split('/')[1])
  });

  console.log(dancers);

  return dancers;
};

const part2 = (input, initialState = undefined) => {
  const numDancers = 16;
  let originalDancers = alphabet.slice(0, numDancers);
  let dancers = initialState ? initialState : alphabet.slice(0, numDancers);

  const instructions = input.split(',').map(x => x.trim());

  const spin = x => dancers = _.reduce(dancers, (res, d, i) => (res + dancers[(i - parseInt(x) + dancers.length) % dancers.length]), '');

  const exchange = (x, y) => {
    const xInt = parseInt(x);
    const yInt = parseInt(y);
    const pivot = dancers[xInt];
    dancers = dancers.replaceAt(xInt, dancers[yInt]);
    dancers = dancers.replaceAt(yInt, pivot);
  };

  const partner = (x, y) => {
    const xInt = dancers.indexOf(x);
    const yInt = dancers.indexOf(y);
    const pivot = dancers[xInt];
    dancers = dancers.replaceAt(xInt, dancers[yInt]);
    dancers = dancers.replaceAt(yInt, pivot);
  };

  const instructionType = {'s': spin, 'x': exchange, 'p': partner};

  const seenStates = [dancers];

  const runInstructions = () => {
    _.each(instructions, i => {
      instructionType[i[0]](i.slice(1, i.length).split('/')[0], i.slice(1, i.length).split('/')[1]);
    });
  };

  const total = 1000000000;

  for(let i =0; i < total; i++) {
    runInstructions();
    if(_.uniq(seenStates).length !== seenStates.length) {
      return (seenStates[(total - i + 1) % seenStates.length]);
    }
    seenStates.push(dancers);
  }

  return dancers;
};

module.exports = {
  part1,
  part2,
};