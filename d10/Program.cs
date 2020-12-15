using System;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

var filename = "./input.txt";
var data = File.ReadAllLines(filename)
    .Select(x => long.Parse(x))
    .OrderBy(x => x)
    .ToList();

data.Add(data.Max() + 3);

long previous = 0;
var hs = new Dictionary<long, long>();

foreach (var x in data)
{
    var diff = x - previous;
    if (!hs.ContainsKey(diff))
    {
        hs[diff] = 0;
    }
    hs[diff] += 1;
    previous = x;
}

foreach (var x in hs)
{
    Console.WriteLine($"{x.Key}:{x.Value}");
}

Console.WriteLine($"Multi: {hs[1] * hs[3]}");
data.Add(0);
var joltages = data.OrderBy(x => x).ToList().GetRange(0, data.Count() - 1);
var steps = new long[joltages.Count()];
steps[0] = 1;
foreach (var i in Enumerable.Range(1, joltages.Count() - 1))
{
    foreach (var j in Enumerable.Range(0, i))
    {
        if (joltages[i] - joltages[j] <= 3)
        {
            steps[i] += steps[j];
        }
    }
}
data = data.OrderBy(x => x).ToList();
var dd = new Dictionary<long, long> { { 0, 1 } };

for (var j = 1; j < data.Count(); ++j)
{
    dd[data[j]] = 0;
}

for (var j = 1; j < data.Count(); ++j)
{
    var dataj = data[j];
    for (var i = 1; i <= 3; ++i)
    {
        if (j - i >= 0)
        {
            var datai = data[j-i];
            if (dataj- datai <= 3 && dd.TryGetValue(datai, out var s))
            {
                dd[data[j]] += s;
            }
        }
    }
}

Console.WriteLine($"{steps.Last()}");

// data.Insert(0, 0);
// long count = 0;


// long perms = 1;
var ranges = data.Select((x, i) =>
{
    var next = data.Skip(i + 1)
        .Where(y => y > x && y - x <= 3)
        .Take(3)
        .Count();
    return next;
}).ToList();



// var p = ranges.Select((x,i)=>{



// })


long perms = 1;
for (int i = 0; i < data.Count();)
{
    var next = ranges[i];
    if (next == 3)
    {
        if (i + 1 < ranges.Count() && ranges[i + 1] == 3)
        {
            perms *= 7;
            i += 3;

        }
        else
        {
            perms *= 4;
            i += 2;
        }
    }
    else if (next == 2)
    {
        i += 1;
        perms *= 2;
    }
    else
    {
        i += 1;
    }
}

//Console.WriteLine(counts.Aggregate((x, y) => x * y));
Console.WriteLine(perms);
//Console.WriteLine(string.Join(',', data));
// Console.WriteLine(string.Join(',', counts));