using NUnit.Framework;

namespace Xpr.xpr.test;

public class XprContextTest
{
    [Test]
    public void TestFunc0()
    {
        var ctx = XprContext.CreateDefault();
        ctx.Funcs0["x"] = () => 1;
        var xpr = new Xpr("x + 1");
        Assert.AreEqual(2, xpr.Eval(ctx));
    }
    
    [Test]
    public void TestFunc1()
    {
        var ctx = XprContext.CreateDefault();
        ctx.Funcs0["x"] = () => 1;
        var xpr = new Xpr("x + 1");
        Assert.AreEqual(2, xpr.Eval(ctx));
    }
}
