import functools
import os


def is_in_order(packet_1, packet_2):
    is_p1_int = isinstance(packet_1, int)
    is_p2_int = isinstance(packet_2, int)
    if is_p1_int and is_p2_int:
        if packet_1 == packet_2:
            return None
        return packet_1 < packet_2

    if is_p1_int or is_p2_int:
        packet_1 = [packet_1] if is_p1_int else packet_1
        packet_2 = [packet_2] if is_p2_int else packet_2
        return is_in_order(packet_1, packet_2)

    shorter = min(len(packet_1), len(packet_2))
    for i in range(0, shorter):
        check = is_in_order(packet_1[i], packet_2[i])
        if check is not None:
            return check

    if len(packet_1) < len(packet_2):
        return True
    elif len(packet_2) < len(packet_1):
        return False
    else:
        return None


def comparator(p1, p2):
    ordered = is_in_order(p1, p2)
    # assume no ties
    return -1 if ordered else 1


def dayX_part1(filename):
    file = open(f'{filename}.txt', 'r')
    lines = [l.strip() for l in file.readlines()]

    ordered_pair_indexes = []

    n_pairs = (len(lines) + 1) // 3
    for i in range(1, n_pairs + 1):
        group_i = (i - 1) * 3
        p1_line = lines[group_i]
        p2_line = lines[group_i + 1]
        if is_in_order(eval(p1_line), eval(p2_line)):
            ordered_pair_indexes.append(i)

    return sum(ordered_pair_indexes)


def dayX_part2(filename):
    file = open(f'{filename}.txt', 'r')
    lines = [l.strip() for l in file.readlines()]

    packets = [eval(l) for l in lines if l.strip() != ""]
    divider_low = [[2]]
    divider_high = [[6]]
    packets.append(divider_low)
    packets.append(divider_high)

    packets = sorted(packets, key=functools.cmp_to_key(comparator))

    return (packets.index(divider_low) + 1) * (packets.index(divider_high) + 1)


def test_day13_example_part1():
    assert (dayX_part1("example")) == 13
    part_1 = dayX_part1("input")
    print(part_1)


def test_day13_example_part2():
    assert (dayX_part2("example")) == 140
    part_2 = dayX_part2("input")
    print(part_2)
