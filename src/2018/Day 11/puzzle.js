const _ = require('lodash');

var fs = require('fs');
var util = require('util');
var log_file = fs.createWriteStream(__dirname + '/debug.log', { flags: 'w' });
var log_stdout = process.stdout;


console.log = function (d) { //
    log_file.write(util.format(d) + '\n');
    log_stdout.write(util.format(d) + '\n');
};


const hundreds = x => ((x % 1000) - (x % 100)) / 100;

const cellPower = (x, y, serial) => {
    const rackId = x + 10;
    const fullPowerLevel = ((rackId * y) + serial) * rackId;
    return hundreds(fullPowerLevel) - 5;
};

let powerCache = {};

const getRegionPower = (grid, x, y, s = 3) => {
    // console.log("" + x + y + s);
    let cacheElement = powerCache[[x, y, s]];
    if (cacheElement) {
        return cacheElement;
    }

    if (s === 1) {
        return grid[[x, y]].power;
    }

    let power;

    if (s % 2 === 0) {
        const topLeft = getRegionPower(grid, x, y, s / 2);
        const topRight = getRegionPower(grid, x + s / 2, y, s / 2);
        const bottomLeft = getRegionPower(grid, x, y + s / 2, s / 2);
        const bottomRight = getRegionPower(grid, x + s / 2, y + s / 2, s / 2);
        power = topLeft + topRight + bottomLeft + bottomRight;
    } else {
        const floor = Math.floor(s / 2);
        const ceil = Math.ceil(s / 2);

        const topLeft = getRegionPower(grid, x, y, ceil);
        const topRight = getRegionPower(grid, x + ceil, y, floor);
        const bottomLeft = getRegionPower(grid, x, y + ceil, floor);
        const bottomRight = getRegionPower(grid, x + floor, y + floor, ceil);


        const intersection = grid[[x + floor, y + floor]].power;

        power = topLeft + topRight + bottomLeft + bottomRight - intersection;
    }

    powerCache[[x, y, s]] = power;
    return power;
};

const part2 = (input, isPart1 = false) => {
    const grid = {};
    let max = {
        power: 0
    };

    for (let x = 1; x < 301; x++) {
        for (let y = 1; y < 301; y++) {
            grid[[x, y]] = {
                power: cellPower(x, y, input)
            };
        }
    }

    for (let x = 1; x < 300; x++) {
        console.log(`${parseFloat(x * 100/300).toFixed(2)}% complete`);
        powerCache = _.filter(powerCache, c => c[0] >= x)
        for (let y = 1; y < 300; y++) {
            let minSize = isPart1 ? 3 : 1;
            const maxSize = isPart1 ? 3 : _.min([300 - x, 300 - y]);
            for (s = minSize; s <= maxSize; s++) {
                const regionPower = getRegionPower(grid, x, y, s);
                // console.log(`${x}-${y}-${s}: ${regionPower}`);
                if (max.power < regionPower) {
                    max = {
                        power: regionPower,
                        x,
                        y,
                        size: s
                    };
                    console.log(`new max: ${x},${y},${s}: ${regionPower}`);
                }
            }
        }
    }

    console.log(max);
    return max;
};

module.exports = {
    part2,
};
