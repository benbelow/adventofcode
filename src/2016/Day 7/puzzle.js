const _ = require('lodash');

const hypernetMatcher = /\[(\w+)]/g;
// Four character palindrom aka ABBA
const palindromeMatcher4 = /(\w)(?!\1\w)(\w)\2\1/;
// 3 character palindrom aka ABA
const palindromeMatcher3 = /(?=(\w)(?!\1)(\w)(\1))/g;

function getMatches(string, regex, index = 1) {
  const matches = [];
  let match;
  while (match = regex.exec(string)) {
    matches.push(match[index]);
  }
  return matches;
}

function getABAMatches(string, regex) {
  const matches = [];
  let match;
  while (match = regex.exec(string)) {
    matches.push(match[1] + match[2] + match[3]);
    string = string.slice(1, string.length);
  }
  return matches;
}

let IPsupportsTLS = function (ip) {
  const hypernet = getMatches(ip, hypernetMatcher);
  const nonHypernets = _.reduce(hypernet, (res, h) => res.replace(h, ' ').replace('[', '').replace(']', ''), ip).split(' ');

  return _.filter(nonHypernets, nh => palindromeMatcher4.test(nh)).length > 0
    && _.filter(hypernet, h => palindromeMatcher4.test(h)).length === 0;
};

const IPsupportsSSL = ip => {
  // return cheatingRegex.test(ip);

  const hypernet = getMatches(ip, hypernetMatcher);
  const nonHypernets = _.reduce(hypernet, (res, h) => res.replace(h, ' ').replace('[', '').replace(']', ''), ip).split(' ');

  let ABAs = function (inputs) {
    return _.flatten(_.filter(_.map(inputs, x => _.map(getABAMatches(x, palindromeMatcher3))), x => !_.isEmpty(x)));
  };

  const nonHypernetABAs = ABAs(nonHypernets);
  const hypernetABAs = ABAs(hypernet);

  if (!nonHypernetABAs || !hypernetABAs) {
    return false;
  }

  return _.filter(hypernetABAs, aba => nonHypernetABAs.includes(abaReverse(aba))).length > 0;
};

const abaReverse = aba => {

  return aba.slice(1, 3) + aba[1];
};

const part1 = (input) => {
  const ips = input.split('\n');
  return _.filter(ips, ip => IPsupportsTLS(ip)).length;
};

const part2 = (input) => {
  const ips = input.split('\n');
  console.log(_.filter(ips, ip => IPsupportsSSL(ip)));
  return _.filter(ips, ip => IPsupportsSSL(ip)).length;
};

module.exports = {
  part1,
  part2,
  IPsupportsTLS,
  IPsupportsSSL,
};