const _ = require('lodash');

const part1 = (playerCount, maxMarble) => {
    let currentPlayer = 0;
    let current = [{
        value: 0
    }];

    current.next = current;
    current.prev = current;

    const addAfter = (value, marble) => {
        const newMarble = {
            value,
            prev: marble,
            next: marble.next,
        };
        marble.next.prev = newMarble;
        marble.next = newMarble;
        return newMarble;
    };

    const score = {};
    for (let p = 0; p < playerCount; p++) {
        score[p] = 0;
    }

    function nextPlayer() {
        currentPlayer++;
        currentPlayer %= playerCount;
    }

    function playTurn(turn) {
        if (turn % 23 === 0) {
            score[currentPlayer] += turn;
            const toRemove = current.prev.prev.prev.prev.prev.prev.prev;
            toRemove.prev.next = toRemove.next;
            toRemove.next.prev= toRemove.prev;
            score[currentPlayer] += toRemove.value;

            current = current.prev.prev.prev.prev.prev.prev;
        } else {
            current = addAfter(turn, current.next)
        }
    }

    for (let turn = 1; turn <= maxMarble; turn++) {
        playTurn(turn);
        nextPlayer();
    }

    return _.max(_.values(score));
};

const part1Old = (playerCount, maxMarble) => {
    let currentPlayer = 0;
    let currentMarbleIndex = 0;
    const state = [0];
    const score = {};
    for (let p = 0; p < playerCount; p++) {
        score[p] = 0;
    }

    function nextPlayer() {
        currentPlayer++;
        currentPlayer %= playerCount;
    }

    function getNextIndex() {
        if (state.length === 1) {
            return 1;
        }
        return (currentMarbleIndex + 2) % state.length;
    }

    function playTurn(turn) {
        let nextIndex = getNextIndex();
        if (turn % 23 === 0) {
            score[currentPlayer] += turn;

            const scoredIndex = ((currentMarbleIndex + state.length) - 7) % state.length;
            score[currentPlayer] += state[scoredIndex];

            state.splice(scoredIndex, 1);

            currentMarbleIndex = scoredIndex;
        } else {
            state.splice(nextIndex, 0, turn);
            currentMarbleIndex = nextIndex;
        }
    }

    for (let turn = 1; turn <= maxMarble; turn++) {
        playTurn(turn);
        nextPlayer();
    }

    console.log(score);
    return _.max(_.values(score));
};

const part2 = (input) => {
    return input;
};

module.exports = {
    part1,
    part2,
};
