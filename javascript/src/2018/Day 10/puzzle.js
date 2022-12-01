const _ = require('lodash');

var fs = require('fs');
var util = require('util');
var log_file = fs.createWriteStream(__dirname + '/debug.log', {flags : 'w'});
var log_stdout = process.stdout;

console.log = function(d) { //
    log_file.write(util.format(d) + '\n');
    log_stdout.write(util.format(d) + '\n');
};

function lineToPoint(l) {
    const halves = l.split('velocity');
    const x = Number(halves[0].split(',')[0].split('<')[1]);
    const y = Number(halves[0].split(',')[1].split('>')[0]);

    const dx = Number(halves[1].split(',')[0].split('<')[1]);
    const dy = Number(halves[1].split(',')[1].split('>')[0]);

    return {
        x, y, dx, dy
    };
}

function draw(minY, maxY, minX, maxX, points, sec) {
    console.log(sec)
    for (let y = minY; y <= maxY; y++) {
        let line = '';
        for (let x = minX; x <= maxX; x++) {
            if (points.some(p => p.x === x && p.y === y)) {
                line += '#';
            } else {
                line += ' ';
            }
        }
        console.log(line);
    }
    console.log('___________________________');
}

const part1 = (input) => {
    const points = input.split('\n').map(lineToPoint);

    let minXDiff;
    let minYDiff;

    for (let i = 0; i < 100000; i++) {
        const maxX = _.max(points.map(p => p.x));
        const minX = _.min(points.map(p => p.x));
        const maxY = _.max(points.map(p => p.y));
        const minY = _.min(points.map(p => p.y));

        let xDiff = maxX - minX;
        let yDiff = maxY - minY;
        if (xDiff < 100 && yDiff < 100) {
            draw(minY, maxY, minX, maxX, points, i);
        } else {
            if (!minXDiff || xDiff < minXDiff) {
                minXDiff = xDiff;
            }
            if (!minYDiff || yDiff < minYDiff) {
                minYDiff = yDiff;
            }
            // console.log(`${minX}, ${maxX}, ${minY}, ${maxY}, "TOO BIG"`)
        }

        points.forEach(p => {
            p.x += p.dx;
            p.y += p.dy;
        });
    }

    console.log(minXDiff)
    console.log(minYDiff)

    return 'hi';
};

const part2 = (input) => {
    return input;
};

module.exports = {
    part1,
    part2,
};
