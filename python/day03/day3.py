import os

priority = 'abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ'


def chunks(xs, n):
    n = max(1, n)
    return (xs[i:i + n] for i in range(0, len(xs), n))


def get_priority(char):
    return priority.index(char) + 1


def day2_part1(filename):
    file = open(f'{filename}.txt', 'r')
    rucksacks = [(line[:len(line) // 2], line[len(line) // 2:len(line) - 1]) for line in file.readlines()]
    mistakes = [set(cs[0]).intersection(set(cs[1])).pop() for cs in rucksacks]

    return sum([get_priority(m) for m in mistakes])


def day2_part2(filename):
    file = open(f'{filename}.txt', 'r')
    lines = [line.strip() for line in file.readlines()]
    groups = [g for g in chunks(lines, 3)]

    badges = [set(g[0]).intersection(set(g[1])).intersection(set(g[2])).pop() for g in groups]

    return sum(get_priority(b) for b in badges)


def test_example_part1():
    assert (day2_part1("example")) == 157
    part1 = day2_part1("input")
    print(part1)


def test_example_part2():
    assert (day2_part2("example")) == 70
    print(day2_part2("input"))
