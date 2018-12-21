const _ = require('lodash');

const TileType = {
    VERTICAL: 'v',
    HORIZONTAL: 'h',
    /// assuming going up i.e. /
    RIGHT_TURN: 'rt',
    LEFT_TURN: 'lt',
    INTERSECTION: 'i',
};

const Orientation = {
    UP: 'u',
    DOWN: 'd',
    LEFT: 'l',
    RIGHT: 'r'
};

const IntersectionTypes = {
    LEFT: 'il',
    RIGHT: 'ir',
    STRAIGHT: 'is',
};

const Clockwise = [
    Orientation.UP,
    Orientation.LEFT,
    Orientation.DOWN,
    Orientation.RIGHT,
];

const IntersectionTypeOrder = [
    IntersectionTypes.LEFT,
    IntersectionTypes.STRAIGHT,
    IntersectionTypes.RIGHT,
];

const RightTurnMap = {
    [Orientation.UP]: Orientation.RIGHT,
    [Orientation.DOWN]: Orientation.LEFT,
    [Orientation.RIGHT]: Orientation.UP,
    [Orientation.LEFT]: Orientation.DOWN
};

const LeftTurnMap = {
    [Orientation.UP]: Orientation.LEFT,
    [Orientation.DOWN]: Orientation.RIGHT,
    [Orientation.RIGHT]: Orientation.DOWN,
    [Orientation.LEFT]: Orientation.UP
};

let cartId = 0;

class Tile {
    constructor(coord, char) {

        this.coord = [...coord];
        this.x = () => this.coord[0];
        this.y = () => this.coord[1];

        this.char = char;

        switch(char) {
            case '/':
                this.type = TileType.RIGHT_TURN;
                break;
            case '\\':
                this.type = TileType.LEFT_TURN;
                break;
            case '|':
            case 'v':
            case '^':
                this.type = TileType.VERTICAL;
                break;
            case '-':
            case '>':
            case '<':
                this.type = TileType.HORIZONTAL;
                break;
            case '+':
                this.type = TileType.INTERSECTION;
                break;
        }
    }
}

class Cart{
    constructor(coord, char) {
        this.id = cartId;
        cartId++;

        this.coord = [...coord];
        this.x = () => this.coord[0];
        this.y = () => this.coord[1];

        this.intersectionCount = 0;

        switch(char) {
            case 'v':
                this.orientation = Orientation.DOWN;
                break;
            case '^':
                this.orientation = Orientation.UP;
                break;
            case '>':
                this.orientation = Orientation.RIGHT;
                break;
            case '<':
                this.orientation = Orientation.LEFT;
                break;
        }
    }

    move(grid) {
        this.step();
        this.changeDirection(grid);

        if (!Object.values(grid).find(g => g.x() === this.x() && g.y() === this.y())) {
            throw new Error();
        }
    }

    step() {
        switch(this.orientation){
            case Orientation.DOWN:
                this.coord[1]++;
                break;
            case Orientation.UP:
                this.coord[1]--;
                break;
            case Orientation.LEFT:
                this.coord[0]--;
                break;
            case Orientation.RIGHT:
                this.coord[0]++;
                break;
        }
    }

    changeDirection(grid) {
        const tile = Object.values(grid).find(g => g.x() === this.x() && g.y() === this.y());

        switch(tile.type) {
            case TileType.HORIZONTAL:
            case TileType.VERTICAL:
                break;
            case TileType.LEFT_TURN:
                this.orientation = LeftTurnMap[this.orientation];
                break;
            case TileType.RIGHT_TURN:
                this.orientation = RightTurnMap[this.orientation];
                break;
            case TileType.INTERSECTION:
                switch(this.intersectionCount % 3){
                    case 0:
                        this.orientation = Clockwise[(Clockwise.indexOf(this.orientation) + 1) % 4];
                        break;
                    case 1:
                        break;
                    case 2:
                        this.orientation = Clockwise[(Clockwise.indexOf(this.orientation) + 3) % 4];
                        break;
                }
                this.intersectionCount++;
        }
    }
}

const part1 = (input) => {
    // return cheat(input);

    let grid = {};
    let carts = [];

    input.split('\n').forEach((line, y) => {
        line.split('').forEach((c, x) => {
            if (c !== ' ') {
                let coord = [x, y];
                grid[coord] = new Tile([x,y], c);
                if ('^v<>'.includes(c)) {
                    carts.push(new Cart([x,y], c))
                }
            }
        });
    });

    const crashedCart = () => carts.find(c => carts.some(c2 => c2.id !== c.id && c.x() === c2.x() && c.y() === c2.y()));

    const hasCartCrashed = (c) => carts.some(c1 => c1.id !== c.id && c.x() === c1.x() && c.y() === c1.y());

    let failsafe = 0;

    let answer;

    while(!answer && failsafe < 999100000000) {
        carts.sort((a, b) => a.x() + a.y() * 1000 - b.x() - b.y() * 1000);
        carts.forEach(c => {
            c.move(grid);
            if (hasCartCrashed(c)) {
                console.log(c);
                answer = {answer: c.coord, tick: failsafe};
            }
        });
        failsafe++;
    }

    return answer;
};

const part2 = (input) => {
    return input;
};

module.exports = {
    part1,
    part2,
};
