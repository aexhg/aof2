

filename = "input.txt"

with open(filename) as f:
    data = [int(x.strip()) for x in f.readlines()]

TOTAL = 2020

def calc(d, const = 0):


    c = [ TOTAL - x - const for x in d]
    di = {x:0 for x in d}

    for x,y in zip(c, d):
        if x in di and x + y + const == TOTAL:
            print(f'{x}, {y}, {x*y}')
            return x,y


for d in data:

    r = calc(data, d)
    if r:
        print(f'{r[0]}, {r[1]}, {d}, {r[0]*r[1]*d}')