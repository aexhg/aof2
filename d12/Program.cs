using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.IO;

var filename = "input.txt";

var data = File.ReadAllLines(filename)
    .Select(x => (Direction: x[0], Count: int.Parse(x.Substring(1))))
    .ToList();

Complex direction = 1;
Complex position = 0;

foreach (var d in data)
{

    Complex move = d.Direction switch
    {

        'N' => new Complex(0, d.Count),
        'S' => new Complex(0, -d.Count),
        'E' => d.Count,
        'W' => -d.Count,
        'F' => d.Count * direction,
        _ => 0
    };
    Complex direct = d.Direction switch
    {
        'L' => new Complex(Math.Cos(Math.PI * d.Count / 180), Math.Sin(Math.PI * d.Count / 180)),
        'R' => new Complex(Math.Cos(-Math.PI * d.Count / 180), Math.Sin(-Math.PI * d.Count / 180)),
        _ => 1
    };

    direction *= direct;
    position += move;
    //Console.WriteLine($"{position}:{direction}");
}



var md = Math.Abs(position.Real) + Math.Abs(position.Imaginary);
Console.WriteLine($"{md}");

var waypoint = new Complex(10, 1);

position = 0;
direction = 1;

foreach (var d in data)
{
    switch(d.Direction){
        case 'N':
            waypoint +=  new Complex(0, 1) * d.Count;
            break;
        case 'S':
            waypoint += new Complex(0, -1) * d.Count;
            break;
        case 'E':
            waypoint += new Complex(1, 0) * d.Count;
            break;
        case 'W':
            waypoint += new Complex(-1, 0) * d.Count;
            break;
        case 'F':
            position += waypoint * d.Count;
            break;
        case 'R':{
            var degrees = -Math.PI * d.Count / 180;
            var rotate = new Complex(Math.Cos(degrees), Math.Sin(degrees));
            waypoint *= rotate;
            break;
        }
        case 'L':{
            var degrees = Math.PI * d.Count / 180;
            var rotate = new Complex(Math.Cos(degrees), Math.Sin(degrees));
            waypoint *= rotate;
            break;
        }
        default:
            break;
    }
   // Console.WriteLine($"{position}:{direction}:{waypoint}");
}


md = Math.Abs(position.Real) + Math.Abs(position.Imaginary);
Console.WriteLine($"{md}");