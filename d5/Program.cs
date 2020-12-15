using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;


var data = File.ReadAllLines("d5/input.txt").ToList();

var ids = data.Select(line => {
    var row = Convert.ToInt32(line.Substring(0, 7).Replace('F', '0').Replace('B','1'), 2);
    var col = Convert.ToInt32(line.Substring(7, 3).Replace('R', '1').Replace('L','0'), 2);

    return row * 8 + col;
});

Console.WriteLine($"max: {ids.Max()}");

var allSeats = new  HashSet<int>(Enumerable.Range(ids.Min(), ids.Max()));
var seats = new HashSet<int>(ids);
allSeats.ExceptWith(seats);
Console.WriteLine($"Missing: {allSeats.First()}");