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
    
    [Test]
    public void TestFunc2()
    {
    
        EvalEq(3, "second(1, 3)", ctx =>
        {
            ctx.Funcs2["second"] = (arg1, arg2) => arg2;
        });
    }
    
    [Test]
    public void TestFuncN()
    {
    
        EvalEq(3, "last(1, 2, 3)", ctx =>
        {
            ctx.FuncsN["last"] = args => args[^1];
        });
    }
}
