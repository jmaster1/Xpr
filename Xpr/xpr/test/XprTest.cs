using NUnit.Framework;

namespace Xpr.xpr.test;

public class XprTest
{

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test()
    {
        Assert.AreEqual(7f, new Xpr("1+2*3").Eval());
        Assert.AreEqual(2f, new Xpr("1+1").Eval());
    }

}