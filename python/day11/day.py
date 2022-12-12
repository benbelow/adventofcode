import math
import os


class Monkey:
    def __init__(self, id, lines, should_divide=True):
        self.id = id
        self.lcm = 1
        self.should_divide = should_divide
        self.inspection_count = 0
        self.items = [int(v.strip()) for v in lines[0].split(":")[1].split(",")]

        op_line = lines[1]
        op_text = op_line.split("new = old")[1].strip()
        operation_type = op_text.split(" ")[0]
        raw_val = op_text.split(" ")[1]
        self.operation = lambda x: x * (x if raw_val == "old" else int(raw_val)) \
            if operation_type == "*" \
            else x + (x if raw_val == "old" else int(raw_val))

        test_line = lines[2]
        self.test_value = int(test_line.split("by ")[1])

        self.true_target = int(lines[3].split("monkey ")[1])
        self.false_target = int(lines[4].split("monkey ")[1])

    def set_lcm(self, lcm):
        self.lcm = lcm

    def catch_item(self, value):
        self.items.append(value)

    def take_turn(self):
        throws = []
        for item in self.items:
            new_value = self.operation(item)
            if self.should_divide:
                new_value = math.floor(new_value / 3)
            else:
                new_value = new_value % self.lcm
            target = self.true_target if new_value % self.test_value == 0 else self.false_target
            self.inspection_count += 1
            throws.append((target, new_value))
        self.items = []
        return throws


def dayX_part1(filename):
    file = open(f'{filename}.txt', 'r')
    lines = [l.strip() for l in file.readlines()]

    monkeys = []
    next_monkey_id = None
    temp_monkey_input = []
    for line in lines:
        if "Monkey" in line:
            next_monkey_id = int(line.split(" ")[1].split(":")[0].strip())
        elif line.strip() == "":
            monkeys.append(Monkey(next_monkey_id, temp_monkey_input))
            temp_monkey_input = []
        else:
            temp_monkey_input.append(line)
    monkeys.append(Monkey(next_monkey_id, temp_monkey_input))

    for ri in range(0, 20):
        for monkey in monkeys:
            throws = monkey.take_turn()
            for throw in throws:
                monkeys[throw[0]].catch_item(throw[1])

    inspection_counts = [m.inspection_count for m in monkeys]
    inspection_counts.sort(reverse=True)

    return inspection_counts[0] * inspection_counts[1]


def dayX_part2(filename):
    file = open(f'{filename}.txt', 'r')
    lines = [l.strip() for l in file.readlines()]

    monkeys = []
    next_monkey_id = None
    temp_monkey_input = []
    for line in lines:
        if "Monkey" in line:
            next_monkey_id = int(line.split(" ")[1].split(":")[0].strip())
        elif line.strip() == "":
            monkeys.append(Monkey(next_monkey_id, temp_monkey_input, should_divide=False))
            temp_monkey_input = []
        else:
            temp_monkey_input.append(line)
    monkeys.append(Monkey(next_monkey_id, temp_monkey_input, should_divide=False))

    lcm = math.lcm(*[m.test_value for m in monkeys])
    for m in monkeys:
        m.set_lcm(lcm)

    for ri in range(0, 10000):
        for monkey in monkeys:
            throws = monkey.take_turn()
            for throw in throws:
                monkeys[throw[0]].catch_item(throw[1])

    inspection_counts = [m.inspection_count for m in monkeys]
    inspection_counts.sort(reverse=True)

    return inspection_counts[0] * inspection_counts[1]


def test_day11_example_part1():
    assert (dayX_part1("example")) == 10605
    part_1 = dayX_part1("input")
    print(part_1)


def test_day11_example_part2():
    assert (dayX_part2("example")) == 2713310158
    part_2 = dayX_part2("input")
    print(part_2)
