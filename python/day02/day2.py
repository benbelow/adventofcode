import os

shapes = ['A', 'B', 'C']


def raw_score(shape):
    match shape:
        case 'X':
            return 1
        case 'Y':
            return 2
        case 'Z':
            return 3
        case 'A':
            return 1
        case 'B':
            return 2
        case 'C':
            return 3
    raise Exception("Unknown shape")


def win(opponent):
    return shapes[(shapes.index(opponent) + 1) % len(shapes)]


def lose(opponent):
    return shapes[(shapes.index(opponent) - 1) % len(shapes)]


def draw(opponent):
    return opponent


def part2_score(shapes_tuple):
    oppo = shapes_tuple[0]
    match shapes_tuple[1]:
        case 'X':
            return 0 + raw_score(lose(oppo))
        case 'Y':
            return 3 + raw_score(draw(oppo))
        case 'Z':
            return 6 + raw_score(win(oppo))


def win_status(shapes_tuple):
    match shapes_tuple:
        case ('A', 'Z') | ('B', 'X') | ('C', 'Y'):
            return 0
        case ('A', 'X') | ('B', 'Y') | ('C', 'Z'):
            return 3
        case ('A', 'Y') | ('B', 'Z') | ('C', 'X'):
            return 6
    raise Exception("Unexpected matchup")


def day2_part1(filename):
    file = open(f'{filename}.txt', 'r')
    rounds = [(line.split()[0], line.split()[1]) for line in file.readlines()]

    return sum([raw_score(r[1]) for r in rounds]) + sum([win_status(r) for r in rounds])


def day2_part2(filename):
    file = open(f'{filename}.txt', 'r')
    rounds = [(line.split()[0], line.split()[1]) for line in file.readlines()]
    return sum([part2_score(r) for r in rounds])


def test_example_part1():
    assert (day2_part1("example")) == 15
    part1 = day2_part1("input")
    assert (part1 < 15128)
    print(part1)


def test_example_part2():
    assert (day2_part2("example")) == 12
    print(day2_part2("input"))
