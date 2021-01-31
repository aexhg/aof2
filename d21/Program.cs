using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;


var filename = "./input.txt";

var data = File.ReadAllLines(filename);

Dictionary<string, HashSet<string>> alergens = new();
Dictionary<int, List<string>> ingredientsList = new();
HashSet<string> allIngredients = new();

for (int i = 0; i < data.Count(); ++i)
{
    var alerg = data[i].Split('(').Last()
        .Replace("contains", string.Empty)
        .Replace(" ", string.Empty).Trim().Trim(')');
    var ingr = data[i].Split('(').First().Trim().Split(' ').Select(x => x.Trim()).ToList();
    ingredientsList[i] = ingr;

    if (!alergens.ContainsKey(alerg))
    {
        alergens[alerg] = new();
    }
    foreach (var ig in ingr)
    {
        alergens[alerg].Add(ig);
        allIngredients.Add(ig);
    }
}

// foreach(var (k, hs) in alergens)
// {
//     var igs = k.Split(',');
//     if(igs.Count() > 1 && igs.All(x = alergens.ContainsKey(x)))
//     {
        
//     }
// }

// List<string> ingredientsNoAlg = new();

// foreach (var (k, ings) in alergens)
// {
//     var igs = k.Split(',');
//     if (igs.Count() > 1 && igs.All(x => alergens.ContainsKey(x)))
//     {
//         HashSet<string> combined = new();
//         foreach (var ig in igs)
//         {
//             combined.UnionWith(alergens[ig]);
//         }

//         var notContained = new HashSet<string>(ings);
//         notContained.ExceptWith(combined);
//         ingredientsNoAlg.AddRange(notContained);

//         foreach (var ig in igs)
//         {
//             HashSet<string> uncombined = new(alergens[ig]);
//             uncombined.ExceptWith(alergens[k]);
//             if (uncombined.Count() > 0)
//             {
//                 foreach (var (k2, v2) in alergens)
//                 {
//                     if (k2 != k && !k.Contains(k2))
//                     {
//                         HashSet<string> modUncombined = new(uncombined);
//                         foreach (var c in uncombined)
//                         {
//                             if (v2.Contains(c))
//                             {
//                                 modUncombined.Remove(c);
//                             }
//                         }
//                         uncombined = new(modUncombined);
//                     }
//                 }
//                 ingredientsNoAlg.AddRange(uncombined);

//             }

//         }
//     }
// }

// ingredientsNoAlg = ingredientsNoAlg.Distinct().ToList();
// int count = 0;
// foreach(var (k, igl) in ingredientsList)
// {
//     var hs1 = new HashSet<string>(igl);
//     var hs2 = new HashSet<string>(ingredientsNoAlg);
//     hs1.IntersectWith(hs2);
//     count += hs1.Count();
// }

// Console.WriteLine(count);

//     // foreach(var (alerg, igs) in alergens)
//     // {
//     //     foreach(var a in igs)
//     //     {
//     //         bool inOthers = false;
//     //         foreach(var (oalerg, oigs) in alergens)
//     //         {
//     //             if(!oalerg.Equals(alerg))
//     //             {
//     //                 if(oigs.Contains(a))
//     //                 {
//     //                     inOthers = true;
//     //                     break;
//     //                 }
//     //             }
//     //             if(inOthers)
//     //             {
//     //                 break;
//     //             }
//     //         }
//     //         if(!inOthers)
//     //         {
//     //             ingredientsNoAlg.Add(a);
//     //         }
//     //     }
//     // }
//     Console.WriteLine("h");
