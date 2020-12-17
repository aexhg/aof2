using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Numerics;

Regex desc = new Regex(@"((\w+(\s)?)+):\s(\d+)\-(\d+)\sor\s(\d+)\-(\d+)");
Regex number = new Regex(@"\d+");
var filename = "./input.txt";
var data = File.ReadAllText(filename);

var startYour = data.IndexOf("your");
var startNearby = data.IndexOf("nearby");

var r1 = data.Substring(startYour, startNearby - startYour);
var r2 = data.Substring(startNearby);
var r3 = data.Substring(0, startYour);

var classifications = r3.Split('\n')
    .Where(s => desc.IsMatch(s))
    .Select(z =>
    {
        var g = desc.Match(z).Groups;
        var k = g[1].Value;
        var lb = int.Parse(g[4].Value);
        var ub = int.Parse(g[5].Value);
        var olb = int.Parse(g[6].Value);
        var oub = int.Parse(g[7].Value);
        return (Desc: k, Lb1: lb, Ub1: ub, Lb2: olb, Ub2: oub);
    }).ToList();

var myNumbers = r1.Split('\n')
    .Where(v => v.Contains(','))
    .Select(y => y.Split(',').Select(z => int.Parse(z))).SelectMany(p => p).ToList();
var otherNumbers = r2
    .Split('\n')
    .Where(v => v.Contains(','))
    .Select(x =>
        x.Split(',').Select(z => int.Parse(z)).ToList()
    )
    .ToList();

int sumInvalid = 0;
List<int> invalidIndex = new List<int>();
for (int i = 0; i < otherNumbers.Count(); ++i)
//foreach (var nb in otherNumbers)
{
    var nb = otherNumbers[i];
    foreach (var nn in nb)
    {
        var iff = classifications.Where(c =>
        {
            if ((c.Lb1 <= nn && nn <= c.Ub1) || (c.Lb2 <= nn && nn <= c.Ub2))
            {
                return true;
            }
            return false;
        });
        if (!iff.Any())
        {
            sumInvalid += nn;
            invalidIndex.Add(i);
        }
    }
}


//part2
var filtered = otherNumbers.Where((x, i) => !invalidIndex.Contains(i)).ToList();
//filtered.Insert(0, myNumbers);


var kclassCols = new Dictionary<string, int>();
var length = filtered.First().Count();
// foreach(var nb in filtered)
// {
//     if(nb.Count() != length){
//         Console.WriteLine("Found question");
//     }
// }

var eliminationDict = new Dictionary<int, IList<(string, int, int, int, int)>>();

for (int i = 0; i < length; ++i)
{
    var de = classifications.Where(c =>
    {
        return filtered.Where(nb => {
            var nn = nb[i];
            if ((c.Lb1 <= nn && nn <= c.Ub1) || (c.Lb2 <= nn && nn <= c.Ub2))
            {
                return true;
            }
            return false;   
        }).Count() == filtered.Count();


    }).ToList();
    eliminationDict[i] = de;
}

Console.WriteLine($"Found entries {kclassCols.Count()}, compared to {classifications.Count()}");

while(kclassCols.Count()!=length)
{
    foreach(var e in eliminationDict){

        if(e.Value.Count()-1 == kclassCols.Count()){
            var entry = e.Value.Where(v => !kclassCols.ContainsKey(v.Item1)).First();
            kclassCols[entry.Item1] = e.Key;
        }

    }
}

long multivalue = 1;

foreach(var c in classifications){

    if(c.Desc.Contains("departure")){
        var i = kclassCols[c.Desc];
        multivalue *= myNumbers[i];
    }

}

Console.WriteLine($"Number invalid: {sumInvalid}");
Console.WriteLine($"Mulitply is {multivalue}");
