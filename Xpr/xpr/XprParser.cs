using System.Runtime.InteropServices;
using NUnit.Framework;

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
    
    //private readonly Stack<XprVal> vals = new();
    
    //private readonly Stack<XprToken?> tokens = new();

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

        //vals.TryPeek(out var prevVal);
        var prevValConsumed = false;
        var prevType = prevVal?.GetValType();
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
                prevValConsumed = func.IsNamed;
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
                                throw new ArgumentOutOfRangeException();
                        }
                    }
                    else
                    {
                        Assert(next != null);
                        arg = next;
                    }
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
        /*
        //
        // push unconsumed token or created val
        if (val == null)
        {
            log("No value created for token: {0}", token);
            //tokens.Push(token);
        }
        else
        {
            log("created value {0} for token {1}", val, token);
            if (prevVal != null)
            {
                var consumed = prevVal.consumeRight(val);
                log("{0}.consumeRight({1}) = {2}", prevVal, val, consumed);
                if (!consumed)
                {
                    consumed = val.consumeLeft(prevVal);
                    log("{0}.consumeLeft({1}) = {2}", val, prevVal, consumed);
                    if (consumed)
                    {
                        vals.Pop();
                    }
                }
                else
                {
                    val = null;
                }
            }

            if (val != null)
            {
                vals.Push(val);
            }
        }

        if (!xt.IsEof)
        {
            ParseNext(xt);
        }
        */
    }
    
    private static void BadInput(XprToken token, XprVal prevVal)
    {
        throw new ArgumentException(string.Format("Bad input, token={}, prevVal={}", token, prevVal));
    }
}