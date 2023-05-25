using Xpr.xpr.Token;
using Xpr.xpr.Util;
using Xpr.xpr.Val;

namespace Xpr.xpr;

public class XprParser : GenericEntity
{
    public static readonly XprParser instance = new();
    
    public static Xpr create(string src)
    {
        return instance.parse(src);
    }
    
    public static XprVal? createVal(string src)
    {
        return instance.parseVal(src);
    }

    private Xpr parse(string source)
    {
        Log("Parsing source: " + source);
        return new Xpr(source)
        {
            Val = parseVal(source),
            Src = source
        };
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
                val = new XprValNumber(token);
                break;
            case XprTokenType.Variable:
                val = new XprValVariable(token);
                break;
            case XprTokenType.BracketOpen:
                var func = new XprValFunc()
                {
                    bracketOpen =  token,
                    nameVal = prevVal?.Cast<XprValVariable>()
                };
                val = func;
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
                    throw new XprParseException($"Unbalanced open bracket for {func} at {func.bracketOpen.Range.From}");
                }
                break;
            case XprTokenType.Operator:
                var mathOp = new XprValMathOp(token);
                mathOp._left = mathOp.RequireVal(prevVal);
                mathOp._right = ParseNext(xt, mathOp, out token);
                Assert(token == null);
                val = mathOp;
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
}