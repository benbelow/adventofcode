const _ = require('lodash');

const part1 = (input) => {
    const coords = input.split('\n').map(s => s.split(',').map(n => Number(n)));

    const minX = _.min(coords.map(c => c[0]));
    const minY = _.min(coords.map(c => c[1]));
    const maxX = _.max(coords.map(c => c[0]));
    const maxY = _.max(coords.map(c => c[1]));

    const sizes = {};
    coords.forEach(c => {
        if(c[0] !== minX && c[0] !== maxX && c[1] !== minY && c[1] !== maxY){
            sizes[c] = 0
        }
    });

    for(let x = minX; x <= maxX; x++){
        for(let y = minY; y <= maxY; y++) {
            const minDistance = _.min(coords.map(c => manhatten(c, [x,y])));
            const nodes = coords.filter(c => manhatten(c, [x,y]) === minDistance);
            if (nodes.length > 1) {
                continue;
            }

            if (sizes[nodes[0]] !== undefined) {
                sizes[nodes[0]]++;
            }
        }
    }

    return _.max(_.values(sizes));
};

const part2 = (input, threshold = 10000) => {
    const coords = input.split('\n').map(s => s.split(',').map(n => Number(n)));

    const minX = _.min(coords.map(c => c[0]));
    const minY = _.min(coords.map(c => c[1]));
    const maxX = _.max(coords.map(c => c[0]));
    const maxY = _.max(coords.map(c => c[1]));

    let answer = 0;

    for(let x = minX; x <= maxX; x++){
        for(let y = minY; y <= maxY; y++) {
            const distances = coords.map(c => manhatten(c, [x,y]));
            if (_.sum(distances) < threshold) {
                answer++;
            }
        }
    }

    return answer;
};

const manhatten = (c1, c2) => {
    return Math.abs(c1[0] - c2[0]) + Math.abs(c1[1] - c2[1]);
};

module.exports = {
    part1,
    part2,
};
