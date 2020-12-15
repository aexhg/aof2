using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Numerics;

string filename = "./input.txt";

const char floor = '.';
const char seat = 'L';
const char occupied = '#';

var map = new Dictionary<Complex, char>();

var data = File.ReadAllLines(filename)
    .ToList();

var neighbours = new List<Complex>{
    1, -1, new Complex(0, 1), new Complex(0, -1), new Complex(1,1), new Complex(1, -1),
    new Complex(-1, 1), new Complex(-1, -1)
};

for (int row = 0; row < data.Count(); ++row)
{
    var srow = data[row];
    for (int col = 0; col < srow.Length; ++col)
    {
        map[new Complex(row, col)] = srow[col];
    }
}

bool stable = false;
int count = 0;
while (!stable)
{
    ++count;
    var round = new Dictionary<Complex, char>(map);

    //print(round);

    foreach (var v in round)
    {
        char var = v.Value switch
        {
            char c when c.Equals(seat) && !occupiedNeighbours2(map, v.Key, 1) => occupied,
            char c when c.Equals(occupied) && true && occupiedNeighbours2(map, v.Key, 5) => seat,
            _ => v.Value
        };

        round[v.Key] = var;
    }

    if (!round.Where(x => map[x.Key] != x.Value).Any())
    {
        stable = true;
        Console.WriteLine($"stable after: {count}");

        var occSeats = round.Where(x => x.Value == occupied).Count();
        Console.WriteLine($"occupied seats: {occSeats}");
    }
    map = new Dictionary<Complex, char>(round);

}

void print(Dictionary<Complex, char> map)
{

    int nrow = (int)map.Max(x => x.Key.Real);
    int ncol = (int)map.Max(x => x.Key.Imaginary);

    for (int row = 0; row <= nrow; ++row)
    {
        var cols = map.Where(x => x.Key.Real == row).Select(y => y.Value);
        Console.WriteLine(string.Join(' ', cols));
    }
    Console.WriteLine();
}

bool occupiedNeighbours2(Dictionary<Complex, char> seats, Complex position, int count)
{

    int ncount = 0;
    foreach (var c in neighbours)
    {
        var d = position + c;
        while (seats.TryGetValue(d, out char value))
        {
            if (value == occupied)
            {
                ncount++;
                break;
            }
            if(value != floor){
                break;
            }
            d += c;
        }
        if (ncount >= count)
        {
            return true;
        }
    }
    return false;

}

bool occupiedNeighbours(Dictionary<Complex, char> seats, Complex position, int count)
{

    int ncount = 0;
    foreach (var c in neighbours)
    {
        var d = position + c;
        if (seats.TryGetValue(d, out char value) && value == occupied)
        {
            ncount++;
        }

        if (ncount >= count)
        {
            return true;
        }
    }
    return false;

}
