using NUnit.Framework;

namespace Xpr.xpr.test;

public class XprContextTest : XprTest
{
    [Test]
    public void TestFunc0()
    {
        EvalEq(9, "x", ctx =>
        {
            ctx.Funcs0["x"] = () => 9;
        });
    }
    
    [Test]
    public void TestFunc1()
    {
    
        EvalEq(3, "eq(2) + 1", ctx =>
        {
            ctx.Funcs1["eq"] = arg1 => arg1;
        });
    }
}
