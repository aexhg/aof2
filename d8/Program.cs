using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;


const string filename = "input.txt";
int accumulated = 0;
var instructionDict = new Dictionary<string, Func<int, int>>(){
    {"acc", (x)=> { accumulated += x; return 1;}},
    {"nop", (x)=> 1},
    {"jmp", (x)=> x}
};

var instructions = File.ReadAllLines(filename)
    .Select(x =>
    {
        var splitted = x.Split(' ');
        return (Instruction: splitted[0], Value: int.Parse(splitted[1]));
    })
    .ToList();

var finished = Run(instructions);
Console.WriteLine($"Acc: {accumulated}");

finished = false;
int currentChangeIndex = 0;
while(!finished){

    accumulated = 0;
    currentChangeIndex = instructions.FindIndex(currentChangeIndex, x => x.Instruction != "acc");
    var changedInstructions = instructions.Select(x => x).ToList();
    (string ins, int val) = changedInstructions[currentChangeIndex];
    var updatedIns = string.Equals(ins, "jmp") ? "nop" : "jmp";
    changedInstructions[currentChangeIndex] = (Instruction: updatedIns, Value: val);
    currentChangeIndex += 1;
    finished = Run(changedInstructions);
    if(finished){
        Console.WriteLine($"Acc: {accumulated}");
    }
}


bool Run(IList<(string Instruction, int Value)> instructions)
{
    var visited = new HashSet<int>();
    int index = 0;
    while (index < instructions.Count())
    {
        (string ins, int val) = instructions[index];
        if (visited.Contains(index))
        {
            return false;
        }
        visited.Add(index);
        index += instructionDict[ins](val);
    }
    return true;
}
