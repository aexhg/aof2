
import copy
filename = "./d3/input.txt"
WIDTH = None
DEPTH = 0
MOVE = complex(3, -1)
TREE = '#'

with open(filename) as f:
    point = complex(0,0)
    grid = {}
    for line in f:
        DEPTH += 1
        if WIDTH is None:
            WIDTH = len(line.strip())
        for ch in line.strip():
            grid[point] = ch
            point += complex(1, 0)
        point += complex(0, -1)
        point = complex(0, point.imag)


def extend(grid, width):
    new_grid = copy.deepcopy(grid)
    for pt, v in grid.items():
        pt += complex(width, 0)
        new_grid[pt] = v
    width += width
    return new_grid

def traverse(grid, width, move):

    position = move
    trees = 0;
    cont = True
    while cont:
#        if position.real >= width:
#            grid = extend(grid, width)
#            width += width
        if grid[position] == TREE:
            trees += 1
        position += move
        position = complex(position.real % width, position.imag)
        if abs(position.imag) >= DEPTH:
            cont = False

    return trees

moves = [
    complex(1, -1),
    complex(3, -1),
    complex(5, -1),
    complex(7, -1),
    complex(1, -2)
]
mult = 1

for m in moves:
    newgrid = copy.deepcopy(grid)
    trees = traverse(newgrid, WIDTH, m)
    print(f'move: {m}, trees hit: {trees}')
    mult *= trees

print(f'mult: {mult}')
