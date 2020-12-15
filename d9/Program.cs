using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

var filename = "input.txt";
int bufferSize = 25;

var data = File.ReadAllLines(filename)
    .Select(x => long.Parse(x))
    .ToList();


Array buffer = Array.CreateInstance(typeof(long), bufferSize);

Array.Copy(data.ToArray(), buffer, bufferSize);


var value = data.Skip(bufferSize)
    .Where(x => {

        var valid = testXMas(buffer, x);
        if(!valid){
            return true;
        }
        updateBuffer(buffer, x);
        return false;
    })
    .First();


Console.WriteLine($"First invalid {value}");

data = data.Where( x => x != value).ToList();

var length = data.Select((x, i) => {

    for(var j = i+2; j < data.Count(); ++j){
        var vals = data.Skip(i).Take(j - i);
        var sum = vals.Sum();
        if(sum == value){
            return (vals.Min(), vals.Max());
        }
        if(sum > value){
            break;
        }
    }
    return (-1, -1);
})
.Where(x => x.Item1 != -1)
.First();

Console.WriteLine($"First {length.Item1}, Last {length.Item2}, Mult: {length.Item2+length.Item1}");

// long slidingWindowSum(IList<long> data, int index, long windowSize, long targret){

//     var rng data.Skip(index).Range(windowSize).Sum();
// }

bool testXMas(Array buffer, long value){

    var hs = new HashSet<long>();
    foreach(long b in buffer){
        var mv = value - b;
        if(hs.Contains(mv)){
            return true;
        }
        hs.Add(b);
    }
    return false;
}

void updateBuffer(Array buffer, long x){

    Array.Copy(buffer, 1, buffer, 0, bufferSize - 1);
    buffer.SetValue(x, buffer.Length - 1);

};