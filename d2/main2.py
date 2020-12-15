
filename = "./d2/input.txt"


with open(filename) as f:
    datain = [x.strip().split(" ") for x in f.readlines()]


def count_valid(data):

    count = 0
    for am, ch, pw in data:
        lower, upper = [int(x) for x in am.split("-")]
        letter = ch[:-1]

        lb = pw[lower-1] == letter
        ub = pw[upper-1] == letter
        if lb != ub:
            count += 1


    return count

print(f'{count_valid(datain)}') 

