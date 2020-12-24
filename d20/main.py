
import numpy as np
import networkx as nx
import itertools
import functools
import operator

filename = "./d20/input.txt"

with open(filename) as f:
    data = f.read()

tiles = data.split('\n\n')
matrices = {}
for t in tiles:
    m = t.split('\n')
    iid = int(m[0].split(' ')[-1].strip()[:-1])
    mat = np.array([ y.strip() for x in m[1:] for y in x]).reshape((len(m[1:]), len(m[1])))
    matrices[iid] = mat



def orientations(mat):

    res = mat
    for f in [0, 1]:
        for i in range(4):
            yield res
            res = np.rot90(res)
        res = np.flip(mat, 1)



def get_borders(mat):
    return [''.join(mat[0, :]), ''.join(mat[:, -1]), ''.join(mat[-1, :]), ''.join(mat[:, 0])]

borders = {}

for n, tile in matrices.items():
    borders[n] = set.union(*[set(get_borders(fl)) for fl in orientations(tile)])
    
#print(borders)
G = nx.Graph()

for i,j in itertools.combinations(borders, 2):

    if(len(borders[i] & borders[j]) > 0):
        G.add_edge(i, j)

#print(nx.to_dict_of_dicts(G))

corners = [x for x in G if len(G[x])==2]
print(functools.reduce(operator.mul, corners))