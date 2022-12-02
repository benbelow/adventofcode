import os

def day1_part1(filename):
    file = open(f'{filename}.txt', 'r')
    lines = file.readlines()

    elves = [0] * len(lines)
    elf_index = 0
    for calories in lines:
        if len(calories.strip()) == 0:
            elf_index += 1
            continue
        elves[elf_index] += int(calories)

    return max(elves)

def day1_part2(filename):
    file = open(f'{filename}.txt', 'r')
    lines = file.readlines()

    elves = [0] * len(lines)
    elf_index = 0
    for calories in lines:
        if len(calories.strip()) == 0:
            elf_index += 1
            continue
        elves[elf_index] += int(calories)

    elves.sort()
    return elves[-1] + elves[-2] + elves[-3]


def test_answer():
    assert (day1_part1("example")) == 24000
    # assert day1("input") == 0
    assert (day1_part2("example")) == 45000
    # assert (day1_part2("input")) == 45000

