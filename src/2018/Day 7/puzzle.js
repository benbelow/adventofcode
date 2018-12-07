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

const part1 = (input) => {
    const instructions = input.split('\n');

    const nodes = {};

    for (let instruction of instructions) {
        const words = instruction.split(' ');
        const node = words[7];
        const preRequisite = words[1];

        if (!nodes[node]) {
            nodes[node] = {name: node, preReq: []}
        }

        if (!nodes[preRequisite]) {
            nodes[preRequisite] = {name: preRequisite, preReq: []}
        }

        nodes[node].preReq.push(preRequisite);
    }

    let answer = [];
    let failsafe = 0;

    while(!_.values(nodes).every(n => n.length === 0) && failsafe < 100) {
        const nextNode = getNextNodes(nodes)[0];

        answer.push(nextNode);
        removeNode(nodes, nextNode);

        failsafe++;
    }

    console.log(nodes);
    return answer.join('');
};

const part2 = (input, workerNum = 5, paddingTime = 60) => {
    const instructions = input.split('\n');

    const nodes = {};

    for (let instruction of instructions) {
        const words = instruction.split(' ');
        const node = words[7];
        const preRequisite = words[1];

        if (!nodes[node]) {
            nodes[node] = {name: node, preReq: []}
        }

        if (!nodes[preRequisite]) {
            nodes[preRequisite] = {name: preRequisite, preReq: []}
        }

        nodes[node].preReq.push(preRequisite);
    }

    let answer = [];
    let failsafe = 100;
    let workers = {};

    for(let w = 0; w < workerNum; w++) {
        workers[w] = {state: 0, node: null};
    }

    for (let i = 0; i < failsafe; i++) {
        for(let w in workers) {
            let nextNodes = _.values(nodes).length > 0 ? getNextNodes(nodes) : [];
            if (workers[w].state <= 0 && nextNodes.length > 0) {
                workers[w].state = paddingTime + alphabet.indexOf(nextNodes[0].toLowerCase()) + 1;
                workers[w].node = nextNodes[0];
            }
            workers[w].state--;

            if (workers[w].state <= 0) {
                removeNode(nodes, workers[w].node);
                workers[w].node = null;
            }
        }
        if (_.values(nodes).length === 0 && _.values(workers).every(w => w <= 0)) {
            return i;
        }
    }


    console.log(nodes);
    return answer.join('');
};

module.exports = {
    part1,
    part2,
};
