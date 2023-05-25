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
    
    private readonly Stack<XprVal> vals = new();
    
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
        ParseNext(xt);
        Assert(vals.Count == 1);
        //Assert(tokens.Count == 0);
        var val = vals.Pop();
        return val;
    }

    XprVal? ParseNext(XprTokenizer xt)
    {
        var token = xt.nextToken();
        if (token == null)
        {
            return null;
        }

        vals.TryPeek(out var prevVal);
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
                break;
            case XprTokenType.BracketClose:
                func = (XprValFunc)prevVal!;
                func.Close(token);
                break;
            case XprTokenType.Operator:
                val = new XprValMathOp(token);
                break;
            case XprTokenType.ArgSeparator:
                break;
            case XprTokenType.Invalid:
            default:
                throw new ArgumentOutOfRangeException();
        }

        if (prevVal != null)
        {
            
            
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