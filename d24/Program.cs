using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;
using System.Collections.Immutable;
using System.Threading.Tasks;

using static System.Console;

var filename = "./input.txt";
var data = File.ReadAllLines(filename);
var rgx = new Regex(@"(nw)|(sw)|(ne)|(se)|(e)|(w)");

HashSet<Vector3> blackTiles = new();

ImmutableDictionary<string, Vector3> moves = new Dictionary<string, Vector3>{
    {"e", new(+1, -1, 0)},
    {"ne", new(+1, 0, -1)},
    {"nw", new(0, +1, -1)},
    {"w", new(-1, +1, 0)},
    {"sw", new(-1, 0, +1)},
    {"se", new(0, -1, +1)},
}.ToImmutableDictionary();

ImmutableList<Vector3> neighbours = new List<Vector3>(){
    new(+1, -1, 0),
    new(+1, 0, -1),
    new(0, +1, -1),
    new(-1, +1, 0),
    new(-1, 0, +1),
    new(0, -1, +1)}.ToImmutableList();

foreach (var l in data)
{
    var matches = rgx.Matches(l);
    Vector3 pt = new();
    foreach (Match m in matches)
    {
        var direction = m.Value;
        pt += moves[direction];
    }
    //WriteLine(pt);
    if (blackTiles.Contains(pt))
    {
        blackTiles.Remove(pt);
    }
    else
    {
        blackTiles.Add(pt);
    }
}

Console.WriteLine(blackTiles.Count());

var nDays = 100;
var dt = DateTime.Now;

for (int i = 0; i <= nDays; ++i)
{
    HashSet<Vector3> inter = new(blackTiles);
    //WriteLine($"{i}:{blackTiles.Count()}");
    foreach(var t in blackTiles)
    {
        {
            var blackNeighbours = neighbours.Where(n =>
            {
                var pt = n + t;
                return blackTiles.Contains(pt);
            }).Count();

            if (blackNeighbours == 0 || blackNeighbours > 2)
            {
                inter.Remove(t);
            }

            var whiteNeighbours = neighbours.Select(n => n + t).Where(x => !blackTiles.Contains(x));

            foreach (var wn in whiteNeighbours)
            {
                var wbNeighbours = neighbours.Select(n => n + wn).Where(x => blackTiles.Contains(x)).Count();
                if (wbNeighbours == 2)
                {
                    inter.Add(wn);
                }
            }
        }
    });
    blackTiles = new(inter);

}

Console.WriteLine(blackTiles.Count());
Console.WriteLine($"{DateTime.Now - dt}");
