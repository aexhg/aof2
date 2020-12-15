
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;


var p = new Day14();
p.Solve();
    public class Day14
    {
        private List<Match> _lines;

        public Day14()
        {
            string pattern = @"(mask = (?<mask>[01X]{36}))|(mem\[(?<memLoc>\d+)\] = (?<val>\d+))";
            
            TextReader tr = new StreamReader("input.txt");
            _lines = tr.ReadToEnd().Split('\n')
                       .Select(line => Regex.Match(line, pattern))
                       .ToList();
        }        

        private void part1()
        {
            Dictionary<long, long> mem = new Dictionary<long, long>();
            (long Or, long And) mask = (0, 0);
            foreach (var line in _lines)
            {
                if (line.Groups["mask"].Success)
                {
                    var m = line.Groups["mask"].Value;
                    mask = (Or: Convert.ToInt64(m.Replace("X", "0"), 2), And: Convert.ToInt64(m.Replace("X", "1"), 2));
                }
                else
                {
                    mem[long.Parse(line.Groups["memLoc"].Value)] = long.Parse(line.Groups["val"].Value) & mask.And | mask.Or;
                }
            }

            Console.WriteLine($"P1: {mem.Values.Sum()}");
        }

        private void writeToMem(string loc, long val, Dictionary<long, long> mem)
        {
            var xes = loc.Select((c, i) => c == 'X' ? i : -1).Where(n => n > -1).ToArray();

            if (xes.Length == 0)
            {
                mem[Convert.ToInt64(string.Concat(loc), 2)] = val;
            }
            else
            {
                var floating = loc.ToCharArray();
                floating[xes[0]] = '1';
                writeToMem(string.Concat(floating), val, mem);
                floating[xes[0]] = '0';
                writeToMem(string.Concat(floating), val, mem);                
            }            
        }

        private void part2()
        {
            Dictionary<long, long> mem = new Dictionary<long, long>();
            string mask = "";
            foreach (var line in _lines)
            {
                if (line.Groups["mask"].Success)
                {
                    mask = line.Groups["mask"].Value;
                }
                else
                {                    
                    // First, flip any bits that are 1 in the mask
                    long or = Convert.ToInt64(mask.Replace('X', '0'), 2);
                    var memLoc = long.Parse(line.Groups["memLoc"].Value) | or;
                    
                    var loc = Convert.ToString(memLoc, 2).PadLeft(36, '0').ToCharArray();
                    var revMask = string.Concat(mask.ToCharArray().Reverse());
                    for (int j = 0; j < revMask.Length; j++)
                    {
                        if (revMask[j] == 'X')
                            loc[^(j + 1)] = 'X';
                    }
                    writeToMem(string.Concat(loc), long.Parse(line.Groups["val"].Value), mem);
                }
            }

            Console.WriteLine($"P2: {mem.Values.Sum()}");
        }

        public void Solve()
        {
            part1();
            part2();
        }

    }
// using System;
// using System.IO;
// using System.Collections;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text.RegularExpressions;

// var filename = "input.txt";
// var memrgx = new Regex(@"mem\[(\d+)\] = (\d+)", RegexOptions.Compiled);
// var maskrgx = new Regex(@"mask = ([X01]+)", RegexOptions.Compiled);
// string mask = "";
// var data = File.ReadAllLines(filename).ToList();

// var register = new Dictionary<long, long>();

// foreach(var l in data){

//     if(maskrgx.IsMatch(l)){
//         mask = maskrgx.Match(l).Groups[1].Value;
//         continue;
//     }

//     var memmatch = memrgx.Match(l).Groups;
//     var memory = long.Parse(memmatch[1].Value);
//     var value = long.Parse(memmatch[2].Value);
//     var valuemem = Convert.ToString(value, 2).PadLeft(36, '0');

//     var newvalue = mask.Zip(valuemem).Select(x => {
//         var mk = x.First;
//         var va = x.Second;
//         char c = mk switch{
//             'X' => va,
//             '1' when va == '1' => '1',
//             '1' when va == '0' => '1',
//             '0' => '0'
//         };
//         return c;
//     }).ToList();


//     long val = Convert.ToInt64(string.Concat(newvalue), 2);
//     register[memory]=val;
// }

// var total = register.Select(x => x.Value).Aggregate((u, v) => u + v);
// Console.WriteLine($"Total in memory: {total}");


// var register2 = new Dictionary<UInt64, long>();

// foreach(var l in data){

//     if(maskrgx.IsMatch(l)){
//         mask = maskrgx.Match(l).Groups[1].Value;
//         continue;
//     }

//     var memmatch = memrgx.Match(l).Groups;
//     var memory = long.Parse(memmatch[1].Value);
//     var value = long.Parse(memmatch[2].Value);
//     var valuemem = Convert.ToString(memory, 2).PadLeft(36, '0');
//     var newvalue = mask.Zip(valuemem).Select(x => {
//         var mk = x.First;
//         var va = x.Second;
//         char c = mk switch{
//             'X' => 'X',
//             '1' => '1',
//             '0' => va,
//             _ => va
//         };
//         return c;
//     }).ToArray();
    
//     var nxs = newvalue.Count(x => x=='X');
//     var nx = Math.Pow(nxs, 2);
//     for(var i = 0; i < nx; ++i){
//         var sx = Convert.ToString(i, 2).PadLeft(nxs, '0');
//         int j = 0; 
//         var memvaluecopy = newvalue.Select(x=>x).ToArray();
//         for(int k = 0; k < memvaluecopy.Count(); ++k){
//             if(memvaluecopy[k]=='X'){
//                 memvaluecopy[k] = sx[j++];
//             }
//         }
//         string memloc = string.Concat(memvaluecopy.Reverse());
//         var memloci = Convert.ToUInt64(memloc, 2);
//         register2[memloci] = value;
//     }

// }



// var newtotal = register2.Select(x => x.Value).Aggregate((u, v) => u + v);
// Console.WriteLine($"Total is {newtotal}");
// // var mask = data.First().Select(c => {
// //     var b = c switch
// //     {
// //         'X' => true,
// //         '0' => false,
// //         '1' => true,
// //         _ => throw new ArgumentException(nameof(c))
// //     };
// //     return b;
// // }).Reverse();

// // var bmast = new BitArray(mask.ToArray());

// // IEnumerable<(UInt32 Location, UInt32 Entry)> entries = data.Skip(1).Select(x => {
// //     var groupd = regex.Matches(x).First().Groups;
// //     return (UInt32.Parse(groupd[1].Value), UInt32.Parse(groupd[2].Value));
// // });

// // var ba = new BitArray(36);

// // foreach(var e in entries){

// //     var baa = BitConverter.GetBytes(e.Entry);
// //     Reverse(baa);
// //     Array.Copy(baa, 0, ba, 32, baa.Count);
// //     var output = bmast.And(baa);

// // }

// void Reverse(BitArray array)
// {
//     int length = array.Length;
//     int mid = (length / 2);

//     for (int i = 0; i < mid; i++)
//     {
//         bool bit = array[i];
//         array[i] = array[length - i - 1];
//         array[length - i - 1] = bit;
//     }    
// }





