import os


def dayX_part1(filename):
    file = open(f'{filename}.txt', 'r')
    lines = [l.strip() for l in file.readlines()]

    ranges = [(
        int(l.split(',')[0].split('-')[0]),
        int(l.split(',')[0].split('-')[1]),
        int(l.split(',')[1].split('-')[0]),
        int(l.split(',')[1].split('-')[1]),
    ) for l in lines]

    targets = [r for r in ranges if
               (r[2] >= r[0] and r[2] <= r[1] and r[3] >= r[0] and r[3] <= r[1])
               or r[0] >= r[2] and r[0] <= r[3] and r[1] >= r[2] and r[1] <= r[3]]

    return len(targets)


def dayX_part2(filename):
    file = open(f'{filename}.txt', 'r')
    lines = [l.strip() for l in file.readlines()]

    ranges = [(
        int(l.split(',')[0].split('-')[0]),
        int(l.split(',')[0].split('-')[1]),
        int(l.split(',')[1].split('-')[0]),
        int(l.split(',')[1].split('-')[1]),
    ) for l in lines]

    targets = [r for r in ranges if
               (r[2] <= r[1] and r[2] >= r[0])
               or (r[3] >= r[0] and r[3] <= r[1])
               or (r[0] >= r[2] and r[0] <= r[3])
               or (r[1] >= r[2] and r[1] <= r[3])
               ]

    return len(targets)


def test_example_part1():
    assert (dayX_part1("example")) == 2
    part_1 = dayX_part1("input")
    assert(part_1 > 554)
    print(part_1)


def test_example_part2():
    assert (dayX_part2("example")) == 4
    part_2 = dayX_part2("input")
    print(part_2)
