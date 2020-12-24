using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;


var filename = "test.txt";

var data = File.ReadAllLines(filename);

long sum = 0;
foreach (var d in data)
{
    var expression = evalBracket(d.Trim().Replace(" ", string.Empty));
    Console.WriteLine($"Value: {expression.chars.First()}");
    sum += long.Parse(expression.chars.First());
}

Console.WriteLine($"Final answer {sum}");



(int pos, IEnumerable<string> chars) evalBracket(string expression)
{
    List<string> vals = new();
    var count = expression.Count();
    int i = 0;
    for (; i < count;)
    {
        var c = expression[i];
        if (Char.IsNumber(c))
        {
            vals.Add(c.ToString());
            ++i;
        }
        if (c == '+' || c == '*' || c =='-' || c =='/')
        {
            vals.Add(c.ToString());
            ++i;
        }
        if (c == '(')
        {
            var evaluated = evalBracket(expression.Substring(i + 1));
            vals.AddRange(evaluated.chars);
            i += evaluated.pos + 1;
        }
        else if (c == ')')
        {
           // var val = evaluateExpr(string.Join(string.Empty, vals));
            var valc = exprBuilder(vals);
            ++i;
            return (i, new List<string>() { valc});
        }
    }
   // var fval = evaluateExpr(string.Join(string.Empty, vals));
    var falc = exprBuilder(vals);
    return (i, new List<string>() { falc });
}

op fop(string f)
{
    op add = (x, y) => x + y;
    op mul = (x, y) => x * y;
    op div = (x, y) => x / y;
    op sub = (x, y) => x - y;
    return f switch
    {
        "+" => add,
        "*" => mul,
        "/" => div,
        "-" => sub,
        _ => null
    };

}

int opPrec(string f)
{
    return f switch
    {
        "+" => 0,
        "-" => 0,
        "/" => 1,
        "*" => 1,
        _ => 10
    };
}

string exprBuilder(IEnumerable<string> expr)
{


    Stack<string> st = new();
    foreach (var s in expr.Reverse())
    {
        st.Push(s);
    }
    return exprBuilderStr(st, 0);

}

string exprBuilderStr(Stack<string> expr, int prec)
{
    string lhs = expr.Pop();
    while (expr.Count() > 1)
    {

        string op = expr.Peek();

        int oprec = opPrec(op);
        if (oprec < prec)
        {
            break;
        }
        op = expr.Pop();
        var rhs = exprBuilderStr(expr, prec + 1);
        var f = fop(op);
        var val = f(long.Parse(lhs), long.Parse(rhs));
        lhs = val.ToString();
    }
    return lhs;

}


public delegate long op(long x, long y);

