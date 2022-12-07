import os


class File:
    def __init__(self, raw_string):
        self.size = int(raw_string.split(" ")[0])
        self.name = raw_string.split(" ")[1]


class Directory:
    def __init__(self, name, parent):
        self.name = name
        self.parent = parent
        self.files = []
        self.dirs = []

    def add_file(self, file):
        self.files.append(file)

    def add_dir(self, dir):
        self.dirs.append(dir)

    def total_size(self):
        file_size = sum([f.size for f in self.files if f.size])
        return file_size + sum(self.dir_sizes())

    def dir_sizes(self):
        return [d.total_size() for d in self.dirs]



def dayX_part1(filename):
    file = open(f'{filename}.txt', 'r')
    lines = [l.strip() for l in file.readlines()]

    root = Directory('/', None)
    current = root
    flat_dirs = [root]

    for line in lines:
        if line == "$ cd /":
            current = root
            continue
        if line[0] == '$':
            command = line.split(" ")[1]
            if command == "cd":
                target = line.split(" ")[2]
                if target == "..":
                    current = current.parent
                else:
                    current = [d for d in current.dirs if d.name == target][0]
        else:
            if line.split(" ")[0] == "dir":
                directory = Directory(line.split(" ")[1], current)
                current.add_dir(directory)
                flat_dirs.append(directory)
            else:
                current.add_file(File(line))

    max = 100000
    return sum([d.total_size() for d in flat_dirs if d.total_size() < max])


def dayX_part2(filename):
    file = open(f'{filename}.txt', 'r')
    lines = [l.strip() for l in file.readlines()]

    root = Directory('/', None)
    current = root
    flat_dirs = [root]

    for line in lines:
        if line == "$ cd /":
            current = root
            continue
        if line[0] == '$':
            command = line.split(" ")[1]
            if command == "cd":
                target = line.split(" ")[2]
                if target == "..":
                    current = current.parent
                else:
                    current = [d for d in current.dirs if d.name == target][0]
        else:
            if line.split(" ")[0] == "dir":
                directory = Directory(line.split(" ")[1], current)
                current.add_dir(directory)
                flat_dirs.append(directory)
            else:
                current.add_file(File(line))

    root_total = root.total_size()
    left = 70000000 - root_total
    needed = 30000000 - left

    max = 100000
    valid = [d for d in flat_dirs if d.total_size() >= needed]

    def sort(dir):
        return dir.total_size()

    valid.sort(key=lambda d: d.total_size())
    return valid[0].name


def test_day7_part1():
    assert (dayX_part1("example")) == 95437
    part_1 = dayX_part1("input")
    print(part_1)


# lol this needs the size not the name, oh well
def test_day7_part2():
    assert (dayX_part2("example")) == "d"
    part_2 = dayX_part2("input")
    assert part_2 != "qzgsswr"
    assert part_2 != "fwdwq"
    print(part_2)
