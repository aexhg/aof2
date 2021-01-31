
filename = "./d21/test.txt"

with open(filename) as f:
    data = f.readlines()

foods = dict()

all_alergies = set()
singular_alergies = dict()

for f in data:
    ingredients = [x.strip() for x in f[:f.find('(')].strip().split(' ')]
    alergens = f[f.find('(contains '):-2].replace('(contains ', '').strip().replace(' ', '')
    foods[alergens] = set(ingredients)
    for x in alergens.split(','):
        all_alergies.add(x)
    if len(alergens.split(',')) == 1:
        singular_alergies[alergens] = ingredients

know_ings_alger={}

while len(know_ings_alger) != len(all_alergies):

    for a, ingredients in foods.items():

        if ',' in a and all(x in singular_alergies for x in a.split(',')):
            for single in a.split(','):
                inter = ingredients.intersection(singular_alergies[single])
                if len(inter) == 1:
                    know_ings_alger[single] = list(inter)[0]
                else:
                    others = set(a.split(',')) - set([single])
                    if all(o in know_ings_alger for o in others):
                        other_ings = set(know_ings_alger[o] for o in others)
                        this_ing = inter - other_ings
                        know_ings_alger[this_ing] = 