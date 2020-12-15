

filename = "input.txt"

with open(filename) as f:
    data = [int(x.strip()) for x in f.readlines()]


TOTAL = 2020

c = [ TOTAL - x for x in data]
d = {x:0 for x in data}

for x,y in zip(c, data):
    if x in d and x + y == TOTAL:
        print(f'{x}, {y}, {x*y}')