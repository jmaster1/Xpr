using Xpr.xpr.Math;
using Xpr.xpr.Token;
using Xpr.xpr.Util;
using Xpr.xpr.Val;

namespace Xpr.xpr;

public class XprParser : GenericEntity
{
    public static readonly XprParser Instance = new();
    
    public static XprVal? createVal(string src)
    {
        return Instance.parseVal(src);
    }

    private XprVal? parseVal(string source)
    {
        var xt = new XprTokenizer(source);
        XprVal? val = null;
        while (!xt.IsEof)
        {
            var next = ParseNext(xt, val, out _);
            if (next != null)
            {
                val = next;
            }
        }
        return val;
    }

    XprVal? ParseNext(XprTokenizer xt, XprVal? prevVal, out XprToken? unconsumedToken)
    {
        unconsumedToken = null;
        var token = xt.nextToken();
        Log($"nextToken={token}");
        if (token == null)
        {
            return null;
        }

        XprVal? val = null;
        switch (token.Type)
        {
            case XprTokenType.Number:
                val = new XprValNumber(token.NumberValue);
                break;
            case XprTokenType.Variable:
                val = new XprValVariable(token.StringValue);
                break;
            case XprTokenType.BracketOpen:
                var name = prevVal?.Cast<XprValVariable>()?.Name;
                var func = new XprValFuncN(name);
                XprVal? arg = null;
                while (!func.IsClosed && !xt.IsEof)
                {
                    var next = ParseNext(xt, arg, out token);
                    if (token != null)
                    {
                        switch (token.Type)
                        {
                            case XprTokenType.BracketClose:
                                if (arg != null)
                                {
                                    func.AddArg(arg);
                                }
                                func.Close(token);
                                break;
                            case XprTokenType.ArgSeparator:
                                func.AddArg(arg);
                                arg = null;
                                break;
                            default:
                                throw new XprParseException($"Unexpected child token {token} for {func}");
                        }
                    }
                    else
                    {
                        Assert(next != null);
                        arg = next;
                    }
                }

                if (!func.IsClosed)
                {
                    throw new XprParseException($"Function {func} left unclosed");
                }

                val = func.Reduce();
                break;
            case XprTokenType.Operator:
                //
                // this might be unary minus if no prevVal
                var op = token.MathOperator;
                if (op == MathOperator.Minus && prevVal == null)
                {
                    var negate = new XprValFunc1(MathFunc1.Negate);
                    arg = ParseNext(xt, null, out token);
                    negate.arg = RequireVal(arg);
                    val = negate;
                }
                else
                {
                    var mathOp = new XprValMathOp(token.MathOperator, prevVal);
                    mathOp._right = ParseNext(xt, mathOp, out token);
                    Assert(token == null);
                    val = mathOp;
                }
                break;
            case XprTokenType.BracketClose:
            case XprTokenType.ArgSeparator:
                break;
            case XprTokenType.Invalid:
            default:
                throw new ArgumentOutOfRangeException();
        }
        Log($"created val={val}");
        if (val == null)
        {
            unconsumedToken = token;
        }
        return val;
    }
    
    private static void BadInput(XprToken token, XprVal prevVal)
    {
        throw new ArgumentException(string.Format("Bad input, token={}, prevVal={}", token, prevVal));
    }
    
    public XprVal? RequireVal(XprVal? val)
    {
        if (val == null)
        {
            throw new XprParseException($"Unexpected end of stream");
        }

        return val;
    }
}