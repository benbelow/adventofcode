import os


def dayX_part1(filename):
    file = open(f'{filename}.txt', 'r')
    lines = [l.strip() for l in file.readlines()]

    return 0


def dayX_part2(filename):
    file = open(f'{filename}.txt', 'r')
    lines = [l.strip() for l in file.readlines()]

    return 0


def test_example_part1():
    assert (dayX_part1("example")) == -1
    part_1 = dayX_part1("input")
    print(part_1)


def test_example_part2():
    assert (dayX_part2("example")) == -1
    part_2 = dayX_part2("input")
    print(part_2)
