import os


def day5_part1(filename):
    example_crates = ['ZN', 'MCD', 'P']
    crates = ['DLVTMHF', 'HQGJCTNP', 'RSDMPH', 'LBVF', 'NHGLQ', 'WBDGRMP', 'GMNRCHLQ', 'CLW', 'RDLQJZMT']

    if filename == "example":
        crates = example_crates
    crates = [list(c) for c in crates]

    file = open(f'{filename}.txt', 'r')
    moves = [l.strip() for l in file.readlines() if "move" in l]

    nums = [(int(m.split(" ")[1]), int(m.split(" ")[3]), int(m.split(" ")[5])) for m in moves]
    for instruction in nums:
        fr = instruction[1] - 1
        to = instruction[2] - 1

        for i in range(0, instruction[0]):
            c = crates[fr].pop()
            crates[to].append(c)

    print(crates)
    final = [c[-1] for c in crates]

    ans = ""
    for f in final:
        ans = ans + f[-1]

    return ans


def day5_part2(filename):
    example_crates = ['ZN', 'MCD', 'P']
    crates = ['DLVTMHF', 'HQGJCTNP', 'RSDMPH', 'LBVF', 'NHGLQ', 'WBDGRMP', 'GMNRCHLQ', 'CLW', 'RDLQJZMT']

    if filename == "example":
        crates = example_crates
    crates = [list(c) for c in crates]

    file = open(f'{filename}.txt', 'r')
    moves = [l.strip() for l in file.readlines() if "move" in l]

    nums = [(int(m.split(" ")[1]), int(m.split(" ")[3]), int(m.split(" ")[5])) for m in moves]
    for instruction in nums:
        fr = instruction[1] - 1
        to = instruction[2] - 1

        temp = []
        for i in range(0, instruction[0]):
            c = crates[fr].pop()
            temp.append(c)

        temp.reverse()
        for x in temp:
            crates[to].append(x)


    print(crates)
    final = [c[-1] for c in crates]

    ans = ""
    for f in final:
        ans = ans + f[-1]

    return ans


def test_example_part1():
    assert (day5_part1("example")) == 'CMZ'
    part_1 = day5_part1("input")
    print(part_1)


def test_example_part2():
    assert (day5_part2("example")) == 'MCD'
    part_2 = day5_part2("input")
    print(part_2)
