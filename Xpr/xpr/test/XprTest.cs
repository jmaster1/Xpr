using NUnit.Framework;

namespace Xpr.xpr.test;

public class XprTest
{

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TestParse()
    {

        var xpr = new Xpr("sin(0+1)").Parse();
        Console.Out.WriteLine(xpr.ToStringDeep());
    }
    
    [Test]
    public void TestParseError()
    {
        CheckParseError(")");
        CheckParseError("sin(");
    }

    private void CheckParseError(string src)
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
    public void Test()
    {
        
        Assert.AreEqual(0f, new Xpr("sin(0)").Eval());
        Assert.AreEqual(7f, new Xpr("1+2*3").Eval());
        Assert.AreEqual(2f, new Xpr("1+1").Eval());
    }

}