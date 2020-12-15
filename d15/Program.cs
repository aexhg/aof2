using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

var filename = "input.txt";
var input = File.ReadAllText(filename)
    .Split(',')
    .Select(x => int.Parse(x))
    .ToList();

var numbers = new Dictionary<int, IList<int>>();

var output = input.First();
var end = 30_000_000;
var t = DateTime.Now;
for (int i = 0; i < end; ++i)
{

    if (i < input.Count())
    {
        output = input[i];
    }
    else
    {

        if (numbers.ContainsKey(output))
        {
            if (numbers[output].Count() > 1)
            {
                var pIt = numbers[output][^1];
                var ppIt = numbers[output][^2];
                output = pIt - ppIt;
            }
            else
            {
                output = 0;
            }
        }
        else
        {
            output = 0;
        }
    }
    if (!numbers.ContainsKey(output))
    {
        numbers[output] = new List<int>();
    }
    numbers[output].Add(i+1);
}

Console.WriteLine(output);
Console.Write($"time : {DateTime.Now - t}");