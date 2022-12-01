const _ = require('lodash');

const actions = {
    BEGIN: 'b',
    WAKE: 'w',
    SLEEP: 's'
};

const part1 = (input) => {
    const data = input.split('\n').map(mapLine);
    const orderedData = _.orderBy(data, ['year', 'month', 'day', 'hour', 'minute']);
    const guardData = getGuardData(orderedData);

    const sleepyGuard = _.maxBy(_.values(guardData), 'minutesAsleep');
    return sleepyGuard.id * sleepyGuard.sleepiestMinute;
};

const part2 = (input) => {
    const data = input.split('\n').map(mapLine);
    const orderedData = _.orderBy(data, ['year', 'month', 'day', 'hour', 'minute']);
    const guardData = getGuardData(orderedData);

    const sleepyGuard = _.maxBy(_.values(guardData), 'sleepiestMinuteValue');
    return sleepyGuard.id * sleepyGuard.sleepiestMinute;
};

const mapLine = (line) => {
    const year = Number(line.split('-')[0].replace('[', ''));
    const month = Number(line.split('-')[1]);
    const day = Number(line.split('-')[2].split(' ')[0]);
    const hour = Number(line.split(':')[0].split(' ')[1]);
    const minute = Number(line.split(':')[1].split(']')[0]);

    let action;
    let id;
    const actionText = line.split('[')[1];

    if (actionText.includes('begins')) {
        action = actions.BEGIN;
        id = Number(line.split('#')[1].split(' ')[0]);
    } else if (actionText.includes('asleep')) {
        action = actions.SLEEP;
    } else if (actionText.includes('wakes')) {
        action = actions.WAKE;
    }

    return {
        year, month, day, hour, minute, id, action
    };
};

const getGuardData = (orderedData) => {
    let result = {};
    const defaultHistogram = () => new Array(60).fill(0);

    let currentId;
    let lastDate;
    let lastMinute = 0;

    orderedData.forEach(d => {
        currentId = d.id || currentId;
        if (!result[currentId]){
            result[currentId] = {id: currentId, histogram: defaultHistogram()}
        }

        if (date(d.year, d.month, d.day) !== lastDate) {
            lastMinute = 0;
            lastDate = date(d.year, d.month, d.day);
        }

        if (d.action === actions.WAKE) {
            for(let i = lastMinute; i < d.minute; i++) {
                result[currentId].histogram[i] =  result[currentId].histogram[i] + 1;
            }
        }

        lastMinute = d.minute;
    });

    _.forEach(result, (v) => {
        v.minutesAsleep = _.sum(v.histogram);
        v.sleepiestMinute = v.histogram.indexOf(_.max(v.histogram));
        v.sleepiestMinuteValue = _.max(v.histogram);
    });

    return result;
};

const date = (y, m, d) => `${y}${m}${d}`;

module.exports = {
    part1,
    part2,
};
