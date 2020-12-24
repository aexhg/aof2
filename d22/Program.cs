using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using static System.Console;

var filename = "./input.txt";

List<int> player1 = new();
List<int> player2 = new();

var data = File.ReadAllText(filename).Split("\n\n");

player1.AddRange(data[0].Split('\n').Skip(1).Select(x => int.Parse(x)).Reverse());
player2.AddRange(data[1].Split('\n').Skip(1).Select(x => int.Parse(x)).Reverse());


var (_, winner) = playRound(player1, player2, true);

int score = 0;
for (int i = 0; i < winner.Count; ++i)
{
    score += (i + 1) * winner[i];
}
WriteLine($"Final score: {score}");


(bool, List<int>) playRound(List<int> player1, List<int> player2, bool recurse = false)
{
    HashSet<int> s1 = new();
    HashSet<int> s2 = new();
    int count = 0;

    const int seed = 487;
    const int modifier = 31;
    while (player1.Count() != 0 && player2.Count() != 0)
    {
        var t1 = player1.Last();
        var t2 = player2.Last();
        player1.RemoveAt(player1.Count - 1);
        player2.RemoveAt(player2.Count - 1);
        var ss1 = player1.Aggregate(seed, (current, item) =>
            (current*modifier) + item.GetHashCode());

        var ss2 = player2.Aggregate(seed, (current, item) =>
            (current*modifier) + item.GetHashCode());

      
        
        bool p1win = false;

        if(s1.Contains(ss1) && s2.Contains(ss2))
        {
            return (true, player1);
        }
        if (recurse && player1.Count() >= t1 && player2.Count() >= t2)
        {
            var sub1 = new List<int>(player1.GetRange(player1.Count - t1, t1));
            var sub2 = new List<int>(player2.GetRange(player2.Count - t2, t2));
            var bb = playRound(sub1, sub2, true);
            p1win = bb.Item1;
        }
        else
        {
            p1win = t1 > t2;
        }

        if (p1win)
        {
            player1.InsertRange(0, new int[] { t2, t1 });
        }
        else
        {
            player2.InsertRange(0, new int[] { t1, t2 });
        }
        ++count;
        s1.Add(ss1);
        s2.Add(ss2);

    }

    bool winner = player1.Count() == 0 ? false : true;
    return (Winner: winner, Deck: winner ? player1 : player2);
}

bool checkPreviousRound(List<int> cards, Dictionary<int, List<int>> previous)
{
    foreach (var (k, l) in previous)
    {
        var ex = l.Except(cards);
        if (ex.Count() == 0)
        {
            return true;
        }
    }
    return false;

}