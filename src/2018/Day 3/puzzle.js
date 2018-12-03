const _ = require('lodash');

function slowPart1(input) {
    const claims = input.split('\n');
    const coordSets = claims.map(claimToCoords);

    const allCoords = _.flatten(coordSets);

    const duplicateCounts = allCoords.map(c => allCoords.filter(cc => cc[0] === c[0] && cc[1] === c[1]).length);

    return duplicateCounts.filter(c => c > 1).reduce((sum, i) => {
        return sum + (1 / i);
    }, 0);
}

const part1 = (input) => {
    const metrics = input.split('\n').map(claimToMetrics);

    const maxX = _.max(metrics.map(m => m.xOffset + m.width));
    const maxY = _.max(metrics.map(m => m.yOffset + m.height));

    const minX = _.min(metrics.map(m => m.xOffset));
    const minY = _.min(metrics.map(m => m.yOffset));

    let count = 0;

    for(let x = minX; x <= maxX; x++ ){
        for(let y = minY; y <= maxY; y++){
            const n = metrics.filter(m => x >= m.xOffset && x < m.xOffset + m.width && y >= m.yOffset && y < m.yOffset + m.height).length;
            if (n > 1) {
                count++;
            }
        }
    }

    return count;
};

const part2 = (input) => {
    return input;
};

const claimToCoords = (claim) => {
    const metrics = claimToMetrics(claim);

    let coords = [];

    for (let x = metrics.xOffset; x < metrics.xOffset + metrics.width; x++) {
        for (let y = metrics.yOffset; y < metrics.yOffset + metrics.height; y++) {
            coords.push([x, y]);
        }
    }

    return coords;
};

const claimToMetrics = (claim) => {

    const offsets = claim.split(' ')[2];
    const dimensions = claim.split(' ')[3];

    const xOffset = Number(offsets.split(',')[0]);
    const yOffset = Number(offsets.split(',')[1].replace(':', ''));

    const width = Number(dimensions.split('x')[0]);
    const height = Number(dimensions.split('x')[1]);

    return {
        xOffset, yOffset, width, height
    }
};

module.exports = {
    part1,
    part2,
};
