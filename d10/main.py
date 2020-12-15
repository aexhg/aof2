import networkx as nx

filename ="input.txt"
data = [int(x) for x in open(filename).readlines()]

G = nx.DiGraph()
data = sorted(data)

data.insert(0, 0)
data.append(max(data))
target = max(data)
for i,x in enumerate(data):
    next = [ y for y in data[i+1:] if y > x and y - x <=3]
    for n in next:
        G.add_edge(x, n)


print('start')
#d= nx.to_dict_of_lists(G);
print(len(list(nx.all_simple_paths(G, 0, target))))

