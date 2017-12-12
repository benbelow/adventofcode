const _ = require('lodash');

const part1 = (input) => {
  let pipes = _.map(input.split('\n'), p => {
    const separated = p.split('<->');
    return {
      start: parseInt(separated[0]),
      ends: _.map(separated[1].split(','), x => parseInt(x)),
    };
  });

  const pipeAt = x => _.find(pipes, p => p.start === x);

  const determineConnections = pipes => {
    return _.map(pipes, p => {
      const visited = [p.start];

      const connections = pipe => {
        visited.push(pipe.start);
        let otherConnections = _.flatten(_.map(_.filter(pipe.ends, p => !visited.includes(p)), x => connections(pipeAt(x))));
        return _.uniq(_.concat([], otherConnections, [pipe.start]));
      };

      return _.merge({}, p, {connected: connections(p)});
    })
  };

  pipes = determineConnections(pipes);

  return _.filter(pipes, p => p.connected.includes(0)).length;
};

const part2 = (input) => {
  let pipes = _.map(input.split('\n'), p => {
    const separated = p.split('<->');
    return {
      start: parseInt(separated[0]),
      ends: _.map(separated[1].split(','), x => parseInt(x)),
    };
  });

  const pipeAt = x => _.find(pipes, p => p.start === x);

  const determineConnections = pipes => {
    return _.map(pipes, p => {
      const visited = [p.start];

      const connections = pipe => {
        visited.push(pipe.start);
        let otherConnections = _.flatten(_.map(_.filter(pipe.ends, p => !visited.includes(p)), x => connections(pipeAt(x))));
        return _.uniq(_.concat([], otherConnections, [pipe.start])).sort();
      };

      return _.merge({}, p, {connected: connections(p)});
    })
  };

  pipes = determineConnections(pipes);

  return _.keys(_.groupBy(pipes, p => p.connected)).length;
};

module.exports = {
  part1,
  part2,
};
