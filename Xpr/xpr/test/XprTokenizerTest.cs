using NUnit.Framework;
using Xpr.xpr;

namespace xpr.test;

public class XprTokenizerTest
{

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test()
    {
        var tokens = new XprTokenizer("1+2").parse();
        AssertToken(tokens[0], XprTokenType.Number, 1);
        AssertToken(tokens[1], XprTokenType.Operator, MathOperator.Plus);
        AssertToken(tokens[2], XprTokenType.Number, 2);
    }

    private void AssertToken(XprToken token, XprTokenType type, object value)
    {
        Assert.AreEqual(type, token.Type);
        Assert.AreEqual(value, token.Value);
    }
}