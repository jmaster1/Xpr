using NUnit.Framework;

namespace Xpr.xpr.test;

public class XprTest
{
    [Test]
    public void TestParseError()
    {
        CheckParseError("-");
        CheckParseError(")");
        CheckParseError("sin(");
    }

    private static void CheckParseError(string src)
    {
        try
        {
            new Xpr(src).Parse();
        }
        catch (XprParseException e)
        {
            Console.WriteLine(e);
            return;
        }
        Assert.Fail();
    }

    [Test]
    public void TestSimple()
    {
        CheckEval("1+2*3", 7);
        CheckEval("1", 1);
        CheckEval("-1", -1);
        CheckEval("1+2", 3);
        CheckEval("1 + 2", 3);
        CheckEval("1+2+3", 6);
        
    }
    
    [Test]
    public void TestMath1()
    {
        CheckEval("cos(0)", 1);
        CheckEval("1 + sin(0)", 1);
        CheckEval("sin(0)", 0);
    }
    
    [Test]
    public void TestMathN()
    {
        CheckEval("sum(1, 2, 3)", 6);
        CheckEval("avg(1, 2, 3)", 2);
    }

    private static void CheckEval(string src, float expectedResult)
    {
        var xpr = new Xpr(src);
        xpr.Parse();
        Console.Out.WriteLine($"{src} > {xpr}");
        var actual = xpr.Eval();
        Assert.AreEqual(expectedResult, actual);
    }
}