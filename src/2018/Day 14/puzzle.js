const _ = require('lodash');

let elf1Index = 0;
let elf2Index = 1;

let recipes = [3, 7];
const elf1 = () => recipes[elf1Index];
const elf2 = () => recipes[elf2Index];

// globals mess up wallaby!
const newRecipes = () => {
    const sum = elf1() + elf2();
    return (sum + '').split('').map(Number);
};

const changeSelectedRecipes = () => {
    elf1Index = (elf1Index + elf1() + 1) % recipes.length;
    elf2Index = (elf2Index + elf2() + 1) % recipes.length;
};

let part2Count = 0;

function tickPart2(input) {
    if (!input) {
        return;
    }

    if (_.last(recipes) === Number(input[part2Count])) {
        part2Count++;
    } else {
        part2Count = _.last(recipes) === Number(input[0]) ? 1 : 0;
    }
    // console.log(input, recipes, part2Count, input[part2Count], input.length);
}

const checkPart2 = (input) => {
    return (part2Count === input.length);
};

const tick = (input) => {
    for (let r of newRecipes()) {
        recipes.push(r);
        tickPart2(input);
    }
    changeSelectedRecipes();
};

let failsafe = 0;
const maxFailsafe = 10;
const isFailsafeOn = false;
const checkFailsafe = () => {
    failsafe++;
    return !isFailsafeOn || failsafe < maxFailsafe;
};


const part1 = (input) => {
    while (checkFailsafe() && recipes.length < input + 10) {
        tick();
    }

    return recipes.slice(input, input + 10).join('');
};

const part2 = (input) => {
    recipes = [3,7];
    part2Count = 0;

    while (checkFailsafe() && !checkPart2(input)) {
        tick(input);
    }
    return recipes.length - 5;
};

module.exports = {
    part1,
    part2,
};
