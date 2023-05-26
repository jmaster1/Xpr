using NUnit.Framework;
using Xpr.xpr.Math;
using Xpr.xpr.Token;

namespace Xpr.xpr.test;

public class XprTokenizerTest : XprTest
{

    [Test]
    public void Test()
    {
        var tokens = new XprTokenizer("1+2").Parse();
        AssertToken(tokens[0], XprTokenType.Number, 1);
        AssertToken(tokens[1], XprTokenType.Operator, MathOperator.Plus);
        AssertToken(tokens[2], XprTokenType.Number, 2);
        
        tokens = new XprTokenizer("3-4").Parse();
        AssertToken(tokens[0], XprTokenType.Number, 3);
        AssertToken(tokens[1], XprTokenType.Operator, MathOperator.Minus);
        AssertToken(tokens[2], XprTokenType.Number, 4);
    }

    private static void AssertToken(XprToken token, XprTokenType type, object value)
    {
        Assert.AreEqual(type, token.Type);
        Assert.AreEqual(value, token.Value);
    }
}
