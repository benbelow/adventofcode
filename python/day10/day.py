import os


def dayX_part1(filename):
    file = open(f'{filename}.txt', 'r')
    lines = [l.strip() for l in file.readlines()]

    interesting_cycles = [20, 60, 100, 140, 180, 220]
    register = 1
    interesting_values = []
    cycle = 1

    def signal_strength():
        return register * cycle

    def check_interesting_value():
        if cycle in interesting_cycles:
            return signal_strength()
        return None

    for line in lines:
        match line.split(" ")[0]:
            case "noop":
                cycle += 1
                interesting_values.append(check_interesting_value())
            case "addx":
                val = int(line.split(" ")[1])
                cycle += 1
                interesting_values.append(check_interesting_value())
                cycle += 1
                register += val
                interesting_values.append(check_interesting_value())
        interesting_values = [i for i in interesting_values if i]

    return sum(interesting_values)


def dayX_part2(filename):
    print("")
    file = open(f'{filename}.txt', 'r')
    lines = [l.strip() for l in file.readlines()]

    register = 1
    cycle = 1

    output = [[]]

    def get_horizontal_pixel_num():
        return (cycle - 1) % 40

    def draw_line():
        if cycle % 40 == 0:
            print("")
            output.append([])

    def draw():
        cycle_debug = cycle
        pixel_x = get_horizontal_pixel_num()
        on = pixel_x in range(register - 1, register + 2)
        pixel = "#" if on else "."
        print(pixel, end='')
        output[-1].append(pixel)
        draw_line()

    for line in lines:
        match line.split(" ")[0]:
            case "noop":
                draw()
                cycle += 1
            case "addx":
                val = int(line.split(" ")[1])
                draw()
                cycle += 1
                draw()
                cycle += 1
                register += val

    return output


def test_day10_example_part1():
    assert (dayX_part1("example")) == 13140
    part_1 = dayX_part1("input")
    print(part_1)


def test_day10_example_part2():
    example = dayX_part2("example")
    assert "".join(example[0]) == "##..##..##..##..##..##..##..##..##..##.."
    part_2 = dayX_part2("input")
    print(part_2)
