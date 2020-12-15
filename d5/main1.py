
filename = "./d5/input.txt"

with open(filename) as f:
    data = f.readlines()


def find_placement(line, size, char):

    start = 0
    for c in line:
        if c == char:
            start = start + size / 2
        size /= 2

    return start

vals= []
for l in data:
    row = find_placement(l.strip()[:-3], 128, 'B')
    col = find_placement(l.strip()[-3:], 8, 'R')
    val = row*8 + col
    vals.append(val)
    #print(f'row={row}, col={col}, val={val}')
    
print(f'max={max(vals)}')

seats = [0]*128*8
for v in vals:
    seats[int(v)] = 1

started = False
for i, x in enumerate(seats):
    if not started and x == 1:
        started = True
    elif started and x == 0:
        print(f'found empty seat: {i}')
        break

# for i in range(0, 128*8, 8):
#     print(f'{i/8}: {seats[i:i+8]}')


