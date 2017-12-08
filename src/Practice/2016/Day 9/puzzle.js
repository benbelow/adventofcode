const _ = require('lodash');

const part1 = (input) => {
  const potentialMarkers = _.reduce(input, (ms, v, i) => {
    if (v === '(') {
      ms.push(i);
      return ms;
    }
    return ms;
  }, []);

  const marker = index => {
    const stringMarker = input.slice(index + 1, input.length).split(')')[0];
    return {
      characterNumber: parseInt(stringMarker.split('x')[0]),
      repetitions: parseInt(stringMarker.split('x')[1]),
    }
  };

  const markers = _.filter(potentialMarkers, (m, i) => {
    let preceedingMarker = potentialMarkers[i-1];
    return preceedingMarker === undefined || preceedingMarker + marker(preceedingMarker).characterNumber < m;
  });

  const markerCharIndexes = _.flatten(_.map(markers, m => _.range(m, m + 5 + marker(m).characterNumber)));

  let decompressed = _.reduce(input, (final, c, i) => {
    if(markers.includes(i)){
      let m = marker(i);
      return final + input.slice(i + 5, i + 5 + parseInt(m.characterNumber)).repeat(m.repetitions);
    } else if (markerCharIndexes.includes(i)) {
      return final;
    }
    return final + c;
  }, '');

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