import os


def marker_index(string):
    buffer = [0,0,0,0]
    for i in range(len(string)):
        char = string[i]
        buffer.append(char)
        buffer = buffer[1:5]
        if len(list(set(buffer))) == len(buffer) and i > 3:
            return i + 1

def message_index(string):
    buffer = [0,0,0,0,0,0,0,0,0,0,0,0,0,0]
    for i in range(len(string)):
        char = string[i]
        buffer.append(char)
        buffer = buffer[1:15]
        if len(list(set(buffer))) == len(buffer) and i > 13:
            return i + 1



def dayX_part1(filename):
    file = open(f'{filename}.txt', 'r')
    lines = [l.strip() for l in file.readlines()]

    return marker_index(lines[0])


def dayX_part2(filename):
    file = open(f'{filename}.txt', 'r')
    lines = [l.strip() for l in file.readlines()]

    return message_index(lines[0])


def test_example_part1():
    assert (dayX_part1("example")) == 7
    part_1 = dayX_part1("input")
    print(part_1)


def test_example_part2():
    assert (dayX_part2("example")) == 19
    part_2 = dayX_part2("input")
    print(part_2)
