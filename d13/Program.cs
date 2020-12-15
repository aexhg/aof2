using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Numerics;

var filename = "test2.txt";
var file = File.ReadAllText(filename);
var startTime = int.Parse(file.Split("\n").First());
var busIds = file
    .Split("\n").Last()
    .Split(',');
var busTimes = busIds
    .Where(x => !x.Equals("x"))
    .Select(y =>
    {
        var t = int.Parse(y);
        var m = (startTime / t) * t + t;
        return (ID: t, Time: m);
    });

var nextBus = busTimes.OrderBy(x => x.Time - startTime).First();
//Console.WriteLine(string.Join(',', busTimes));

Console.WriteLine($"Next Bus at {nextBus.Time}, ID {nextBus.ID}: {nextBus.ID * (nextBus.Time - startTime)}");

List<(long Id, long Idx)> buses = new List<(long Id, long Idx)>();
for (long i = 0; i < busIds.Count(); ++i)
{
    if (long.TryParse(busIds[i], out long val))
    {
        buses.Add((val, i));
    }
}

long N = buses.Select(x => x.Id).Aggregate((u, v) => u * v);

var factors = buses.Select( b => {
    var z = N / b.Id;
    long p =0;
    for(var i = 0; i < z; ++i){
        if( (z * i) % b.Id == 1){
            p = i;
            break;
        }
    }
    return (Z: z, P: p);
});

var t = buses.Zip(factors).Select(x => 
    (x.First.Id - x.First.Idx) * x.Second.Z * x.Second.P
);

var T = t.Sum() % N;

Console.WriteLine($"Time {T}");
