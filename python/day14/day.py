import math
import os


def dayX_part1(filename):
    file = open(f'{filename}.txt', 'r')
    lines = [l.strip() for l in file.readlines()]

    rocks = parse_rocks(lines)
    sands = set([])

    next_sand = None
    while next_sand is None or next_sand[1] < math.inf:
        next_sand = place_sand_infinite_void(rocks, sands)
        sands.add(next_sand)

    return len(sands) - 1


def place_sand_infinite_void(rocks, sands):
    sand_source = (500, 0)

    max_y = max([r[1] for r in rocks])
    min_y = min([r[1] for r in rocks] + [s[1] for s in sands])

    taken = rocks.union(sands)

    # big fall for efficiency
    sand = (sand_source[0], min_y - 1)
    while sand[1] < max_y:
        down = (sand[0], sand[1] + 1)
        left = (sand[0] - 1, sand[1] + 1)
        right = (sand[0] + 1, sand[1] + 1)
        down_blocked = down in taken
        left_blocked = left in taken
        right_blocked = right in taken

        if not down_blocked:
            sand = down
            continue
        elif not left_blocked:
            sand = left
            continue
        elif not right_blocked:
            sand = right
            continue
        else:
            return sand

    return sand[0], math.inf


def place_sand_floor(rocks, sands):
    sand_source = (500, 0)

    max_y = max([r[1] for r in rocks])
    min_y = min([r[1] for r in rocks] + [s[1] for s in sands])

    taken = rocks.union(sands)

    # big fall for efficiency
    sand = (sand_source[0], min_y - 1)
    while sand[1] < max_y + 10:
        down = (sand[0], sand[1] + 1)
        left = (sand[0] - 1, sand[1] + 1)
        right = (sand[0] + 1, sand[1] + 1)
        down_blocked = down in taken or down[1] >= max_y + 2
        left_blocked = left in taken or left[1] >= max_y + 2
        right_blocked = right in taken or right[1] >= max_y + 2

        if not down_blocked:
            sand = down
            continue
        elif not left_blocked:
            sand = left
            continue
        elif not right_blocked:
            sand = right
            continue
        else:
            return sand

    raise Exception("infinite void should not exist")


def parse_rocks(lines):
    rocks = set([])
    for line in lines:
        vertices = []
        for sub_line in line.split(" -> "):
            vertices.append((int(sub_line.split(",")[0]), int(sub_line.split(",")[1])))
        for i in range(0, len(vertices) - 1):
            src = vertices[i]
            des = vertices[i + 1]

            x_range = range(src[0], des[0] + 1) if src[0] <= des[0] else range(des[0], src[0] + 1)
            y_range = range(src[1], des[1] + 1) if src[1] <= des[1] else range(des[1], src[1] + 1)

            for x in x_range:
                for y in y_range:
                    rocks.add((x, y))
    return rocks


pyramid_numbers = [0]


def pyramid_number(i):
    if len(pyramid_numbers) > i:
        return pyramid_numbers[i]
    for j in range(len(pyramid_numbers), i + 1):
        pyramid_numbers.append(j * 2 - 1)
    return pyramid_number(i)


def pyramid_size(i):
    pyramid_number(i)
    return sum(pyramid_numbers[0:i])


def dayX_part2(filename):
    file = open(f'{filename}.txt', 'r')
    lines = [l.strip() for l in file.readlines()]

    rocks = parse_rocks(lines)
    sands = set([])

    max_y = max([r[1] for r in rocks])
    triangle_height = max_y + 3
    triangle_base = pyramid_number(triangle_height)
    sand_triangle = pyramid_size(triangle_height)

    hidden = set([])
    half_base = (triangle_base - 1) // 2
    print()
    for y in range(0, max_y + 2):
        for x in range(500 - half_base, 500 + half_base + 1):
            if (x, y) in rocks:
                print('#', end='')
            else:
                print(".", end="")
            up = (x, y - 1)
            left = (x - 1, y - 1)
            right = (x + 1, y - 1)
            if (x, y) not in rocks \
                    and (up in rocks or up in hidden) \
                    and (left in rocks or left in hidden) \
                    and (right in rocks or right in hidden):
                hidden.add((x, y))
        print("")


    return sand_triangle - len(rocks) - len(hidden)


def test_day14_example_part1():
    assert (dayX_part1("example")) == 24
    part_1 = dayX_part1("input")
    print(part_1)


def test_day14_example_part2():
    assert (dayX_part2("example")) == 93
    part_2 = dayX_part2("input")
    assert part_2 < 24813
    print(part_2)
