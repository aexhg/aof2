using System.IO;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System;


var filename = "./input.txt";
var input = File.ReadAllText(filename).Split('\n');
{
    HashSet<Vector4> active = new();

    for (int i = 0; i < input.Count(); ++i)
    {
        for (int j = 0; j < input[i].Count(); ++j)
        {
            if (input[i][j] == '#')
            {
                active.Add(new(i, j, 0, 0));
            }
        }
    }


    List<Vector4> neighbours = new();
    for (int i = -1; i <= 1; ++i)
    {
        for (int j = -1; j <= 1; ++j)
        {
            for (int k = -1; k <= 1; ++k)
            {
                for(int p = -1; p <=1; ++p){
                if (!(i == 0 && j == 0 && k == 0 && p == 0))
                {
                    neighbours.Add(new(i, j, k,p));
                }
                }
            }
        }
    }

    int rounds = 6;


    for (int i = 0; i < rounds; ++i)
    {

        HashSet<Vector4> cpy = new();

        foreach (var d in active)
        {
            var activeN = neighbours.Where(p => active.Contains(p + d)).Count();
            var inactivateN = neighbours.Where(p => !active.Contains(p + d)).Select(x => (x + d)).Distinct();


            if (activeN == 2 || activeN == 3)
            {
                cpy.Add(d);
            }

            foreach (var n in inactivateN)
            {
                var activeNN = neighbours.Where(p => active.Contains(p + n)).Count();
                if (activeNN == 3)
                {
                    cpy.Add(n);
                }
            }
        }

        active = new(cpy);

    }

    Console.WriteLine($"Active: {active.Count()}");

}
{
      HashSet<Vector3> active = new();

    for (int i = 0; i < input.Count(); ++i)
    {
        for (int j = 0; j < input[i].Count(); ++j)
        {
            if (input[i][j] == '#')
            {
                active.Add(new(i, j, 0));
            }
        }
    }


    List<Vector3> neighbours = new();
    for (int i = -1; i <= 1; ++i)
    {
        for (int j = -1; j <= 1; ++j)
        {
            for (int k = -1; k <= 1; ++k)
            {
                if (!(i == 0 && j == 0 && k == 0))
                {
                    neighbours.Add(new(i, j, k));
                }
            }
        }
    }

    int rounds = 6;


    for (int i = 0; i < rounds; ++i)
    {

        HashSet<Vector3> cpy = new();

        foreach (var d in active)
        {
            var activeN = neighbours.Where(p => active.Contains(p + d)).Count();
            var inactivateN = neighbours.Where(p => !active.Contains(p + d)).Select(x => (x + d)).Distinct();


            if (activeN == 2 || activeN == 3)
            {
                cpy.Add(d);
            }

            foreach (var n in inactivateN)
            {
                var activeNN = neighbours.Where(p => active.Contains(p + n)).Count();
                if (activeNN == 3)
                {
                    cpy.Add(n);
                }
            }
        }

        active = new(cpy);

    }

    Console.WriteLine($"Active: {active.Count()}");
}

