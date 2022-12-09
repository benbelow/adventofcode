import os


def dayX_part1(filename):
    file = open(f'{filename}.txt', 'r')
    lines = [l.strip() for l in file.readlines()]

    length = len(lines)
    width = len(lines[0])

    grid = {}

    for y in range(length):
        line = lines[y]
        for x in range(width):
            grid[(x, y)] = int(line[x])

    def is_visible(coord):
        x, y = coord
        height = grid[(x, y)]
        # edge
        if x == 0 or y == 0 or x == width - 1 or y == length - 1:
            return True
        blockers_l = [x2 for x2 in range(0, x) if grid[(x2, y)] >= height]
        blockers_r = [x2 for x2 in range(x + 1, width) if grid[(x2, y)] >= height]

        blockers_u = [y2 for y2 in range(0, y) if grid[(x, y2)] >= height]
        blockers_d = [y2 for y2 in range(y + 1, length) if grid[(x, y2)] >= height]

        visible = blockers_r == [] or blockers_l == [] or blockers_u == [] or blockers_d == []
        return visible

    visibles = [c for c in grid.keys() if is_visible(c)]
    return len(visibles)


def dayX_part2(filename):
    file = open(f'{filename}.txt', 'r')
    lines = [l.strip() for l in file.readlines()]

    length = len(lines)
    width = len(lines[0])

    grid = {}

    for y in range(length):
        line = lines[y]
        for x in range(width):
            grid[(x, y)] = int(line[x])

    def scenic_score(coord):
        x, y = coord
        height = grid[(x, y)]
        # edge
        if x == 0 or y == 0 or x == width - 1 or y == length - 1:
            return 0
        blockers_l = [height - grid[(x2, y)] for x2 in range(0, x)]
        blockers_r = [height - grid[(x2, y)] for x2 in range(x + 1, width)]

        blockers_u = [height - grid[(x, y2)] for y2 in range(0, y)]
        blockers_d = [height - grid[(x, y2)] for y2 in range(y + 1, length)]

        def view(blocker_list):
            visible_edge = [b for b in blocker_list if b <= 0] == []
            v = 0 if visible_edge else 1
            for b in blocker_list:
                if b > 0:
                    v += 1
                else:
                    return v
            return v

        blockers_l.reverse()
        blockers_u.reverse()

        l = view(blockers_l)
        r = view(blockers_r)
        u = view(blockers_u)
        d = view(blockers_d)
        score = l * r * u * d
        return score

    scores = [scenic_score(c) for c in grid]
    return max(scores)


def test_example_day8_part1():
    assert (dayX_part1("example")) == 21
    part_1 = dayX_part1("input")
    print(part_1)


def test_example_day8_part2():
    assert (dayX_part2("example")) == 8
    part_2 = dayX_part2("input")
    print(part_2)
