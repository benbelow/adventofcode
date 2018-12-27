const _ = require('lodash');

const part1 = (input) => {
    let elf1Index = 0;
    let elf2Index = 1;

    let recipes = [3, 7];
    const elf1 = () => recipes[elf1Index];
    const elf2 = () => recipes[elf2Index];

    const newRecipes = () => {
        const sum = elf1() + elf2();
        return (sum + '').split('').map(Number);
    };

    const changeSelectedRecipes = () => {
        elf1Index = (elf1Index + elf1() + 1) % recipes.length;
        elf2Index = (elf2Index + elf2() + 1) % recipes.length;
    };

    const tick = () => {
        for (let r of newRecipes()) {
            recipes.push(r);
        }
        changeSelectedRecipes();
    };

    let failsafe = 0;
    const maxFailsafe = 100000;
    const isFailsafeOn = false;
    const checkFailsafe = () => {
        failsafe++;
        return !isFailsafeOn || failsafe - 1 < maxFailsafe;
    };

    while (checkFailsafe() && recipes.length < input + 10) {
        tick();
        failsafe++;
    }

    console.log(recipes);
    return recipes.slice(input, input + 10).join('');
};

const part2 = (input) => {
    return input;
};

module.exports = {
    part1,
    part2,
};
