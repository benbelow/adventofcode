const _ = require('lodash');

function getNextNodes(nodes) {
  // console.log(nodes, _.minBy(_.values(nodes), n => n.preReq.length));
  const minLen = _.minBy(_.values(nodes), n => n.preReq.length).preReq.length;

  return _.values(nodes).filter(n => n.preReq.length === minLen).map(x => x.name).sort();
}

const alphabet = 'qwertyuiopasdfghjklzxcvbnm'.split('').sort();

function removeNode(nodes, nextNode) {
  delete nodes[nextNode];
  _.values(nodes).forEach(n => {
    _.remove(n.preReq, p => p === nextNode);
  });
}

let parseNodes = function (instructions) {
  const nodes = {};
  for (let instruction of instructions) {
    const words = instruction.split(' ');
    const node = words[7];
    const preRequisite = words[1];

    if (!nodes[node]) {
      nodes[node] = { name: node, preReq: [] }
    }

    if (!nodes[preRequisite]) {
      nodes[preRequisite] = { name: preRequisite, preReq: [] }
    }

    nodes[node].preReq.push(preRequisite);
  }
  return nodes;
};

const part1 = (input) => {
  const instructions = input.split('\n');

  const nodes = parseNodes(instructions);

  let answer = [];
  let failsafe = 0;

  while (!_.values(nodes).every(n => n.length === 0) && failsafe < 100) {
    const nextNode = getNextNodes(nodes)[0];

    answer.push(nextNode);
    removeNode(nodes, nextNode);

    failsafe++;
  }

  return answer.join('');
};

const part2 = (input, workerNum = 5, paddingTime = 60) => {
  const instructions = input.split('\n');

  const nodes = parseNodes(instructions);

  const workers = [];
  for (let i = 0; i < workerNum; i++) {
    workers.push({ id: i, timeLeft: 0, node: null });
  }

  function assignNode(worker, node) {
    worker.node = node;
    worker.timeLeft += alphabet.indexOf(node.toLowerCase()) + 1 + paddingTime;
  }

  let assignWorkers = function () {
    const validNodes = _.values(nodes)
      .filter(n => n.preReq.length === 0)
      .filter(n => !workers.some(w => w.node === n.name))
      .map(n => n.name)
      .sort();
    while (workers.some(w => w.node === null) && validNodes.length > 0) {
      const worker = workers.find(w => w.node === null);
      assignNode(worker, validNodes[0]);
      validNodes.shift();
    }
  };

  function finishNode(node) {
    removeNode(nodes, node);
  }

  function doWork() {
    workers.forEach(w => {
      if (w.node) {
        w.timeLeft--;
      }
      if (w.timeLeft <= 0) {
        finishNode(w.node);
        w.node = null;
      }
    })
  }

  for (let i = 1; i < 10000; i++) {
    if (workers.some(w => w.node === null)) {
      assignWorkers();
    }
    doWork();
    if (_.values(nodes).length === 0) {
      return i;
    }
  }
};

module.exports = {
  part1,
  part2,
};
