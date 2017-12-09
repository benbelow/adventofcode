const _ = require('lodash');

const part1 = (input) => {

  const marker = index => {
    const stringMarker = input.slice(index + 1, input.length).split(')')[0];
    return {
      characterNumber: parseInt(stringMarker.split('x')[0]),
      repetitions: parseInt(stringMarker.split('x')[1]),
    }
  };

  let ignoreNum = 0;
  const markers = _.reduce(input, (markers, c, i) => {
    if (ignoreNum > 0) {
      ignoreNum--;
      return markers;
    }

    if (c !== '(') {
      return markers;
    }

    if (c === '(') {
      ignoreNum += parseInt(input.slice(i, input.length).split('x')[0].split('(')[1]);
      return _.merge(markers, markers.push(i));
    }

    return markers;
  }, [] );

  const markerCharIndexes = _.flatten(_.map(markers, m => {
    let mm = marker(m);
    const markerLength = 3 + mm.characterNumber.toString().length + mm.repetitions.toString().length;
    return _.range(m, m + markerLength + (mm.characterNumber))
  }));


  let decompressed = _.reduce(input, (final, c, i) => {
    if(markers.includes(i)){
      let m = marker(i);
      const markerLength = 3 + m.characterNumber.toString().length + m.repetitions.toString().length;
      return final + input.slice(i + markerLength, i + markerLength + parseInt(m.characterNumber)).repeat(m.repetitions);
    } else if (markerCharIndexes.includes(i)) {
      return final;
    }
    return final + c;
  }, '');

  console.log(_.reduce(markers, (s, m) => {
    console.log(s, m, marker(m));
    return s + (parseInt(marker(m).characterNumber) * parseInt(marker(m).repetitions))
  }, 0));

  console.log(decompressed);
  return decompressed.length;
};

const part2 = (input) => {
  return input;
};

module.exports = {
  part1,
  part2,
};