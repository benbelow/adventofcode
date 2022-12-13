import math
import os

alpha = 'abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ'


def get_or_default(dict, key):
    return dict[key] if key in dict.keys() else None


class Node:

    def __init__(self, coord, height):
        self.coord = coord
        self.height = height
        self.connected = []


def dayX_part1(filename):
    file = open(f'{filename}.txt', 'r')
    lines = [l.strip() for l in file.readlines()]

    start = (0, 0)
    end = (0, 0)
    nodes = {}

    for y in range(0, len(lines)):
        line = lines[y]
        for x in range(0, len(line)):
            c = (x, y)
            char = line[x]
            if char == "S":
                start = c
                char = "a"
            elif char == "E":
                end = c
                char = "z"
            height = alpha.index(char)
            nodes[c] = Node(c, height)

    for coord in nodes.keys():
        x = coord[0]
        y = coord[1]
        this_node = nodes[(x, y)]
        line = lines[y]

        up_neighbour = get_or_default(nodes, (x, y - 1))
        down_neighbour = get_or_default(nodes, (x, y + 1))
        left_neighbour = get_or_default(nodes, (x - 1, y))
        right_neighbour = get_or_default(nodes, (x + 1, y))

        neighbours = [n for n in [up_neighbour, down_neighbour, left_neighbour, right_neighbour] if n is not None]

        for n in neighbours:
            if n.height - this_node.height <= 1:
                this_node.connected.append(n)

    def dijkstra():
        initial_node = nodes[start]
        unvisited = [n for n in nodes.keys()]
        tentative_distances = {node.coord: 0 if node.coord == start else math.inf for node in nodes.values()}
        current = initial_node

        while end in unvisited:
            for neighbour in [n for n in current.connected if n.coord in unvisited]:
                new_tentative_distance = tentative_distances[current.coord] + 1
                old_tentative_distance = tentative_distances[neighbour.coord]
                tentative_distances[neighbour.coord] = min(new_tentative_distance, old_tentative_distance)

            unvisited.remove(current.coord)
            unvisited.sort(key=lambda nod: tentative_distances[nod])
            current = nodes[unvisited[0]] if len(unvisited) > 0 else None
            xxx = 0
        return tentative_distances[end]

    return dijkstra()


def dayX_part2(filename):
    file = open(f'{filename}.txt', 'r')
    lines = [l.strip() for l in file.readlines()]

    end = (0, 0)
    nodes = {}

    for y in range(0, len(lines)):
        line = lines[y]
        for x in range(0, len(line)):
            c = (x, y)
            char = line[x]
            if char == "S":
                start = c
                char = "a"
            elif char == "E":
                end = c
                char = "z"
            height = alpha.index(char)
            nodes[c] = Node(c, height)

    for coord in nodes.keys():
        x = coord[0]
        y = coord[1]
        this_node = nodes[(x, y)]
        line = lines[y]

        up_neighbour = get_or_default(nodes, (x, y - 1))
        down_neighbour = get_or_default(nodes, (x, y + 1))
        left_neighbour = get_or_default(nodes, (x - 1, y))
        right_neighbour = get_or_default(nodes, (x + 1, y))

        neighbours = [n for n in [up_neighbour, down_neighbour, left_neighbour, right_neighbour] if n is not None]

        for n in neighbours:
            if n.height - this_node.height <= 1:
                this_node.connected.append(n)

    def dijkstra(start, short_circuit):
        initial_node = nodes[start]
        unvisited = [n for n in nodes.keys()]
        tentative_distances = {node.coord: 0 if node.coord == start else math.inf for node in nodes.values()}
        current = initial_node

        while end in unvisited:
            if tentative_distances[current.coord] >= short_circuit:
                return None
            for neighbour in [n for n in current.connected if n.coord in unvisited]:
                new_tentative_distance = tentative_distances[current.coord] + 1
                old_tentative_distance = tentative_distances[neighbour.coord]
                tentative_distances[neighbour.coord] = min(new_tentative_distance, old_tentative_distance)

            unvisited.remove(current.coord)
            unvisited.sort(key=lambda nod: tentative_distances[nod])
            current = nodes[unvisited[0]] if len(unvisited) > 0 else None
        return tentative_distances[end]

    current_shortest = math.inf
    for n in [node for node in nodes.values() if node.height == 0]:
        path = dijkstra(n.coord, current_shortest)
        if path is not None:
            current_shortest = path

    return current_shortest


def test_day12_example_part1():
    assert (dayX_part1("example")) == 31
    part_1 = dayX_part1("input")
    print(part_1)

# takes 1 min to run lol.
def test_day12_example_part2():
    assert (dayX_part2("example")) == 29
    part_2 = dayX_part2("input")
    print(part_2)
