filename = "./d7/input.txt"

import networkx as nx

G = nx.DiGraph(name="bags")

with open(filename) as f:
    data = f.readlines()
    for l in data:
        
        words = l.strip().split(' ')
        colour = '_'.join(words[:2])

        G.add_node(colour)

        if 'contain no other' in l:
            count = 0
        else:
            other_colour = '_'.join(words[5:7])
            count = int(words[4])

            G.add_node(colour)
            G.add_edge(colour, other_colour, bag_count=count)
        more_colours = l.strip().split(',')
        if len(more_colours):
            for ec in more_colours[1:]:
                ec_words = ec.strip().split(' ')
                other_colour = '_'.join(ec_words[1:3])
                count = int(ec_words[0])
                
                G.add_node(other_colour)
                G.add_edge(colour, other_colour, bag_count=count)


my_bag = 'shiny_gold'
paths = nx.shortest_path(G, target=my_bag, weight="bag_count")
# ncolours = set()
# for p, v in paths.items():
#     ncolours.add(p)
#     for pp in v:
#         ncolours.add(pp)
print(f'Number paths {len(paths)-1}')


paths = []
bags = 1

dd = nx.to_dict_of_dicts(G)

def count_bags(start, adj_dict):
    bags = 1

    for bag, vals in adj_dict[start].items():
        bags += vals['bag_count'] * count_bags(bag, adj_dict)
    return bags

print(count_bags(my_bag, dd)-1)