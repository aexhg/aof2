using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

const string filename = "input.txt";

var data = File.ReadAllText(filename)
    .Replace("\n\n", "--")
    .Split("--");

var counts = data
    .Select(l => new HashSet<char>(l.Replace("\n", string.Empty).ToArray()))
    .Select(x => x.Count())
    .Sum();

Console.WriteLine($"Sum is {counts}");

var intersections = data
    .Select(l => l.Split("\n"))
    .Select(p => {
        var hs = new HashSet<char>(p.First());
        var rest = p.Skip(1);
        if (rest.Any())
        {
            var ohs = 
                rest.Select(x => new HashSet<char>(x.ToCharArray()));
            foreach(var oh in ohs){
                hs.IntersectWith(oh);
            }
        }
        return hs;
    })
    .Select(z => z.Count())
    .Sum();

Console.WriteLine($"interesetction: {intersections}");



// foreach(var g in intersections){
//     HashToString(g);
// }

void HashToString(HashSet<char> collection){
    Console.Write("{");
    foreach (int i in collection)
    {
        Console.Write(" {0}", i);
    }
    Console.WriteLine(" }");
}
