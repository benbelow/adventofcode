const _ = require('lodash');

function trim(s, c) {
    if (c === "]") c = "\\]";
    if (c === "\\") c = "\\\\";
    return s.replace(new RegExp(
        "^[" + c + "]+|[" + c + "]+$", "g"
    ), "");
}

String.prototype.replaceAt = function (index, replacement) {
    return this.substr(0, index) + replacement + this.substring(index + replacement.length, this.length + replacement.length);
};

const part1 = (input, generations = 20, log = console.log) => {
    let countdown = 5;

    const initialState = input.split('\n')[0].split(' ')[2];
    let offset = 0;
    let additions = 0;
    let state = initialState;

    const cache = {};

    const rules = input.split('\n').slice(2).map(r => ({
        pattern: r.split(' ')[0],
        result: r.split(' ')[2]
    })).map(r => {
        const replacement = r.pattern.slice(0, 2) + r.result + r.pattern.slice(2);
        return { ...r, replacement };
    });

    for (let i = 0; i < generations; i++) {
        if (state.substring(0, 5).includes('#')) {
            state = "....." + state;
            offset += 5;
        }

        if (state.substring(state.length - 5).includes('#')) {
            state = state + ".....";
            additions += 5;
        }

        const lastState = "" + state;
        let nextState = "" + state;

        lastState.split('').forEach((pot, i) => {
            if (i < 2 || i > state.length - 1) {

            } else {
                let s = state.slice(i - 2, i + 3);
                const rule = rules.find(r => r.pattern === s);
                if (rule) {
                    nextState = nextState.replaceAt(i, rule.result);
                } else {
                    nextState = nextState.replaceAt(i, '.');
                }
            }
        });
        // console.log(i, offset, additions);

        state = nextState;
        if (cache[trim(state, '.')]) {
            const loopStateStart = cache[trim(state, '.')];
            // log(loopStateStart.index);
            // log(loopStateStart.value);
            // log(state);
            // log(loopStateStart.value.length);
            // log(state.length);
            // log(loopStateStart.offset);
            // log(offset);
            // log(loopStateStart.additions);
            // log(additions);

            const loopStart = loopStateStart.index;


            return loopStateStart.value.split('').reduce((sum, pot, i) => {
                const potNum = i - offset + (generations - loopStateStart.index);
                const isPlant = pot === '#';
                return sum + (potNum * (isPlant ? 1 : 0));
            }, 0);
        }
        cache[trim(state, '.')] = { value: state, index: i, offset: offset, additions: additions };
    }

    return state.split('').reduce((sum, pot, i) => {
        const potNum = i - offset;
        const isPlant = pot === '#';
        return sum + (potNum * (isPlant ? 1 : 0));
    }, 0);
};

const newpart1 = (input, generations = 20) => {
    const initialState = input.split('\n')[0].split(' ')[2];
    const rules = input.split('\n').slice(2).map(r => ({
        pattern: r.split(' ')[0],
        result: r.split(' ')[2]
    })).map(r => {
        const replacement = r.pattern.slice(0, 2) + r.result + r.pattern.slice(2);
        return { ...r, replacement };
    });

    class Pot {
        constructor({ next = null, prev = null, isPlant = false, initialIndex = 0 }) {
            // console.log(next, prev)
            this.next = next;
            this.prev = prev;
            this.isPlant = isPlant;
            if (next && prev) {
                throw new Error();
            }
            if (next) {
                this.index = next.index - 1;
            } else if (prev) {
                this.index = prev.index + 1;
            } else {
                this.index = initialIndex;
            }
        }

        getNext() {
            return this.next ? this.next : new Pot({prev: this});
        };

        getPrev() {
            if (!this.prev) {
                this.prev = new Pot({ next: this });
            }
            return this.prev;
        };

        getCharacter() {
            return this.isPlant ? '#' : '.';
        };

        getPattern() {
            return this.getPrev().getPrev().getCharacter()
                + this.getPrev().getCharacter()
                + this.getCharacter()
                + this.getNext().getCharacter()
                + this.getNext().getNext().getCharacter();
        };

        fullString() {
            return this.next ? this.getCharacter() + this.next.fullString() : this.getCharacter();
        }

        queueNext() {
            const rule = rules.find(r => r.pattern === this.getPattern());
            this.nextState = rule ? rule.result === '#' : false;
            if (this.next) {
                this.next.queueNext();
            }
        }

        tick() {
            this.isPlant = this.nextState;
            this.nextState = null;
            if (this.next) {
                this.next.tick();
            }
        }
    }

    const state = initialState.split('').map((pot, i) => {
        return {
            isPlant: pot === '#',
            index: i
        };
    });

    const node = state.reverse().reduce((next, pot) => {
        return new Pot({ next, isPlant: pot.isPlant, initialIndex: pot.index });
    }, null);

    console.log(node.fullString());

    node.queueNext();
    node.tick();

    console.log(node.fullString());
};

module.exports = {
    part1,
};
