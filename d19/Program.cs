using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

var filename = "./input.txt";
var rgx = new Regex(@"""([a-z])""");

var input = File.ReadAllLines(filename);
var listRules = input
    .Where(l => l.Contains(':'))
    .ToDictionary(
        k => int.Parse(k.Split(':').First()),
        v => {
            var vv = v.Split(':')[1];
            return vv switch {
               var s when s[1] is '"' => s[2..^1],
                var s => s[1..] 
            };
        });

var data = input.Where(l => !l.Contains(':')).ToList();

Dictionary<int, List<string>> rules = new();
SimplifyRules(listRules);
listRules[8] = "c";
listRules[11] = "d";
var rule0 = listRules[0];

 if (true)
    {
        // Rule8  ==> (Rule42)+  [one or more]
        // Rule11 ==>  Rule42{k}Rule31{k} where k >= 1 [balanced group]
        rule0 = rule0
            .Replace("c", $"(?:{listRules[42]})+")
            .Replace("d", $"(?<DEPTH>{listRules[42]})+(?<-DEPTH>{listRules[31]})+(?(DEPTH)(?!))");
    }

var rule0Regex = new Regex($"^{rule0}$", RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace);

var count = data.Count(rule0Regex.IsMatch);
Console.WriteLine($"Count is {count}");
void SimplifyRules(IDictionary<int, string> dict)
{
    var done = new HashSet<int>();
    foreach (var (key, val) in dict)
        if (val.Length == 1)
            done.Add(key);
    
    while (done.Count != dict.Count)
    {
        foreach (var (key, val) in dict)
        {
            if (done.Contains(key))
                continue;

            var remain = false;
            dict[key] = Regex.Replace(val, @"\d+", m =>
            {
                var mKey = int.Parse(m.Value);
                if (done.Contains(mKey))
                    return $"(?:{dict[mKey]})";

                remain = true;
                return m.Value;
            });

            if (!remain)
                done.Add(key);
        }
    }
}



// while (rules.Count() < listRules.Count())
// {
//     foreach (var l in listRules)
//     {
//         if (!rules.ContainsKey(l.Key))
//         {
//             if (rgx.IsMatch(l.Value))
//             {
//                 var match = rgx.Match(l.Value);
//                 rules[l.Key] = new() { match.Groups[1].Value };
//             }
//             else
//             {
//                 HashSet<int> requiredRules = new();
//                 var everyRule = l.Value.Split('|').Select(x => x.Trim().Split(' '));
//                 foreach (var o in everyRule)
//                 {
//                     foreach (var i in o)
//                     {
//                         requiredRules.Add(int.Parse(i.Trim()));
//                     }
//                 }

//                 HashSet<int> containedRules = new(rules.Select(x => x.Key));
//                 if (requiredRules.IsSubsetOf(containedRules))
//                 {
//                     rules[l.Key] = new();
//                     var val1 = l.Value.Split('|');
//                     var val2 = val1
//                         .Select(v => v.Trim().Split(' ').Select(y => int.Parse(y)));
//                         //.Select(z => z.Select(y => rules[y]))
//                         //.ToList();

//                     foreach(var newrule in val2)
//                     {
//                         rules[l.Key] = new();
//                         var conditions = newrule.Select(x => rules[x]).ToList();
//                         var fst = conditions.First();
//                         var lst = conditions.Skip(1);
//                         foreach(var ff in fst)
//                         {
//                             foreach(var ll in lst)
//                             {
//                                 foreach(var lp in ll)
//                                 {
//                                     rules[l.Key].Add(ff+lp);
//                                     //Console.WriteLine(ff+lp);
//                                 }
//                             }
//                         }
//                     }

//                 }
//             }
//         }
//     }
// }