import os


def dayX_part1(filename):
    file = open(f'{filename}.txt', 'r')
    lines = [l.strip() for l in file.readlines()]

    head = (0, 0)
    tail = (0, 0)
    seen_tails = {(0, 0)}

    def move_head(instruction, head):
        direction = instruction.split(" ")[0]
        magnitude = int(instruction.split(" ")[1])

        x = head[0]
        y = head[1]

        steps = []
        match direction:
            case "R":
                return ([(x + dx, y) for dx in range(1, magnitude + 1)])
            case "L":
                return ([(x - dx, y) for dx in range(1, magnitude + 1)])
            case "U":
                return ([(x, y - dy) for dy in range(1, magnitude + 1)])
            case "D":
                return ([(x, y + dy) for dy in range(1, magnitude + 1)])
        return steps

    def move_tail(head, tail):
        hx = head[0]
        hy = head[1]
        tx = tail[0]
        ty = tail[1]

        xdiff = hx - tx
        ydiff = hy - ty

        x_makeup = 0
        y_makeup = 0

        if xdiff == 0 and ydiff == 0:
            return tx, ty

        if xdiff == 0:
            y_makeup = ydiff - 1 if ydiff > 0 else ydiff + 1
        elif ydiff == 0:
            x_makeup = xdiff - 1 if xdiff > 0 else xdiff + 1
        elif abs(xdiff) + abs(ydiff) > 2:
            x_makeup = 1 if xdiff > 0 else -1
            y_makeup = 1 if ydiff > 0 else -1

        return tx + x_makeup, ty + y_makeup

    for i in lines:
        steps = move_head(i, head)
        for s in steps:
            head = s
            tail = move_tail(head, tail)
            seen_tails.add(tail)

    return len(seen_tails)


def dayX_part2(filename):
    file = open(f'{filename}.txt', 'r')
    lines = [l.strip() for l in file.readlines()]

    head = (0, 0)
    tails = [(0,0) for i in range(0,10)]
    seen_tails = {(0, 0)}

    def move_head(instruction, head):
        direction = instruction.split(" ")[0]
        magnitude = int(instruction.split(" ")[1])

        x = head[0]
        y = head[1]

        steps = []
        match direction:
            case "R":
                return ([(x + dx, y) for dx in range(1, magnitude + 1)])
            case "L":
                return ([(x - dx, y) for dx in range(1, magnitude + 1)])
            case "U":
                return ([(x, y - dy) for dy in range(1, magnitude + 1)])
            case "D":
                return ([(x, y + dy) for dy in range(1, magnitude + 1)])
        return steps

    def move_tail(head, tail):
        hx = head[0]
        hy = head[1]
        tx = tail[0]
        ty = tail[1]

        xdiff = hx - tx
        ydiff = hy - ty

        x_makeup = 0
        y_makeup = 0

        if xdiff == 0 and ydiff == 0:
            return tx, ty

        if xdiff == 0:
            y_makeup = ydiff - 1 if ydiff > 0 else ydiff + 1
        elif ydiff == 0:
            x_makeup = xdiff - 1 if xdiff > 0 else xdiff + 1
        elif abs(xdiff) + abs(ydiff) > 2:
            x_makeup = 1 if xdiff > 0 else -1
            y_makeup = 1 if ydiff > 0 else -1

        return tx + x_makeup, ty + y_makeup

    for i in lines:
        steps = move_head(i, head)
        for s in steps:
            head = s
            for it in range(0, len(tails)):
                if it == 0:
                    tails[0] = move_tail(head, tails[0])
                else:
                    tails[it] = move_tail(tails[it - 1], tails[it])
                if it == 8:
                    seen_tails.add(tails[it])


    return len(seen_tails)


def test_day9_example_part1():
    assert (dayX_part1("example")) == 13
    part_1 = dayX_part1("input")
    print(part_1)


def test_day9_example_part2():
    assert (dayX_part2("example")) == 1
    assert (dayX_part2("example2")) == 36
    part_2 = dayX_part2("input")
    print(part_2)
