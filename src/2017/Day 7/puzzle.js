const _ = require('lodash');

const inputToTower = input => {
  let lines = input.split('\n');
  return _.map(lines, l => {
    const name = _.first(l.split('(')).trim();
    const weight = parseInt(l.split('(')[1].split(')')[0]);
    const postArrow = _.first(_.drop(l.split('->')));
    return {name, weight, parents: postArrow && _.map(postArrow.split(', '), p => p.trim())}
  });
};

const bottomOfTower = towerPrograms => {
  const names = _.map(towerPrograms, 'name');
  const allParents = _.flatten(_.filter(_.map(towerPrograms, 'parents')));

  return _.find(names, n => !allParents.includes(n) && getProgram(towerPrograms, n).parents);
};

const getProgram = (tower, programId) => {
  return _.find(tower, p => p.name === programId);
};

const part1 = (input) => {
  const programs = inputToTower(input);
  return bottomOfTower(programs);
};

const part2 = (input) => {
  const programs = inputToTower(input);
  const bottom = getProgram(programs, bottomOfTower(programs));

  const towerWeight = baseProgram => {
    if(!baseProgram.parents) {
      return baseProgram.weight;
    }
    let parents = _.map(baseProgram.parents, x => getProgram(programs, x));
    let parentsWeights = _.map(parents, p => towerWeight(p));
    return baseProgram.weight + _.sum(parentsWeights);
  };

  const problemProgram = baseProgram => {
    if(!baseProgram.parents) {
      return baseProgram;
    }

    let subTowers = _.map(baseProgram.parents, parentName => {
      const parent = getProgram(programs, parentName);
      return({baseProgram: parentName, weight: towerWeight(parent)});
    });

    // NB this won't necessarily work when there are only two branches that could be the problem. Thankfully this didn't happen in the real input, but worth being aware of
    let problemTowers = (_.filter(_.groupBy(subTowers, s => s.weight), v => v.length === 1));
    if (_.isEmpty(problemTowers)) {
      return baseProgram;
    }

    return problemProgram(getProgram(programs, problemTowers[0][0].baseProgram));
  };

  let pp = problemProgram(bottom);
  const wrongTowerWeight = towerWeight(pp);
  let wrongTowerChild = _.find(programs, p => {
    return p.parents && p.parents.includes(pp.name);
  });
  const rightTowerWeight = towerWeight(getProgram(programs, _.find(wrongTowerChild.parents, p => p !== pp.name)));

  return pp.weight - (wrongTowerWeight - rightTowerWeight);
};

module.exports = {
  part1,
  part2,
};