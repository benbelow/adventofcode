const _ = require('lodash');

function getMatches(string, regex, index) {
  index || (index = 1); // default to the first capturing group
  const matches = [];
  let match;
  while (match = regex.exec(string)) {
    matches.push(match[index]);
  }
  return matches;
}

let IPsupportsTLS = function (ip) {
  const hypernetMatcher = /\[(\w+)]/g;
  const palindromeMatcher = /(\w)(?!\1\w)(\w)\2\1/;

  const hypernet = getMatches(ip, hypernetMatcher);
  const nonHypernets = _.reduce(hypernet, (res, h) => res.replace(h, ' ').replace('[', '').replace(']', ''), ip).split(' ');

  return _.filter(nonHypernets, nh => palindromeMatcher.test(nh)).length > 0
    && _.filter(hypernet, h => palindromeMatcher.test(h)).length === 0;
};

const part1 = (input) => {
  const ips = input.split('\n');
  console.log(ips.length);
  console.log(_.filter(ips, i => IPsupportsTLS(i)).length);
  console.log(_.filter(ips, i => !IPsupportsTLS(i)).length);
  return _.filter(ips, ip => IPsupportsTLS(ip)).length;
};

const part2 = (input) => {
  return input;
};

module.exports = {
  part1,
  part2,
  IPsupportsTLS
};