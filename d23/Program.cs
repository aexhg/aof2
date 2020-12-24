using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using static System.Console;

var filename = "input.txt";
var nums = File.ReadAllText(filename).Select(x => (long)Char.GetNumericValue(x));
var input = new LinkedList<long>(nums);
long max  = input.Max() + 1;
for(long l = max; l <= 1_000_000; ++l)
{
    input.AddLast(l);
}
var t = DateTime.Now;
Moves(input, 10_000_000);
//WriteLine(string.Join(",", input));

var idx = input.Find(1);

WriteLine(idx.Value);
var idxm1=idx.Next;
if(idxm1 == null)
{
    idxm1 = input.First;
}
var idxm2 = idxm1.Next;

WriteLine(idxm1.Value * idxm2.Value);
WriteLine($"{DateTime.Now - t}");

// long count = 0;
// string output = default;
// while(count < input.Count-1)
// {
//     if(idx == input.Count)
//     {
//         idx = 0;
//     }
//     output += input[idx];
//     ++count;
//     ++idx;
// }
// WriteLine(output);


void Moves(LinkedList<long> numbers, int N)
{
    var idx = numbers.First;
    int rounds = 0;

    Dictionary<long, LinkedListNode<long>> nodes = new();
    var node = numbers.First;
    while(node != null)
    {
        nodes[node.Value] = node;
        node = node.Next;
    }

    var max = numbers.Max();
    var min = numbers.Min();
    var hs = new long[3];
    idx = numbers.First;
    while(rounds < N)
    {
        long n = idx.Value;
        var destination = n - 1;
        for(int i = 0; i < 3; ++i)
        {
            var t = (idx.Next ?? numbers.First).Value;
            hs[i] = t;
            numbers.Remove(idx.Next ?? numbers.First);
            nodes.Remove(t);
        }

        bool findDestination = hs.Contains(destination) || !nodes.ContainsKey(destination);

        while(findDestination)
        {
            destination -= 1;
            if(destination < min)
            {
                destination = max;
            }
            findDestination = hs.Contains(destination) || !nodes.ContainsKey(destination);
        }

        var destIdx = nodes[destination];
        foreach(var v in hs)
        {
            destIdx = numbers.AddAfter(destIdx, v);
            nodes[v] = destIdx;

        }
        //idx = numbers.IndexOf(n) + 1;
        idx = idx.Next ?? numbers.First;
        ++rounds;
    }

    

}
