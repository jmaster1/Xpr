namespace Xpr.xpr;

public class XprParser : Logger
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
    
    private readonly Stack<XprToken?> tokens = new();

    private Xpr parse(string source)
    {
        log("Parsing source: " + source);
        return new Xpr(source)
        {
            Val = parseVal(source),
            Src = source
        };
    }

    private XprVal? parseVal(string source)
    {
        var xt = new XprTokenizer(source);
        var val = ParseNext(xt);
        if(!xt.IsEof) {
            throw new Exception(string.Format("Unexpected remaining text '{}' in source expression '{}'",
                source.Substring(xt.Cur), source));
        }
        return val;
    }

    XprVal? ParseNext(XprTokenizer xt)
    {
        var token = xt.nextToken();
        if (token == null)
        {
            return null;
        }
        XprVal? val = null;
        tokens.TryPeek(out var prevToken);
        vals.TryPeek(out var prevVal);
        switch (token.Type)
        {
            case XprTokenType.Number:
                val = new XprValNumber(token);
                break;
            case XprTokenType.BracketOpen:
                //
                // this must be function if variable is prev
                XprToken? nameToken = null;
                if (prevVal != null && prevVal.Is(XprValType.Variable))
                {
                    nameToken = ((XprValVariable)prevVal).Token;
                }
                val = new XprValFunc(nameToken, token);
                break;
            case XprTokenType.BracketClose:
                if (!prevVal.Is(XprValType.Func))
                {
                    BadInput(token, prevVal);
                }
                var func = (XprValFunc)prevVal;
                func.Close(token);
                break;
            case XprTokenType.Operator:
                val = new XprValMathOp(token, prevVal);
                break;
            case XprTokenType.Variable:
                val = new XprValVariable(token);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        //
        // push unconsumed token or created val
        if (val == null)
        {
            log("No value created for token: {0}", token);
            tokens.Push(token);
        }
        else
        {
            log("created value {0} for token {1}", val, token);
            if (prevVal != null)
            {
                prevVal.consumeRight(val);
            }

            vals.Push(val);
        }

        if (!xt.IsEof)
        {
            ParseNext(xt);
        }
        return val;
    }

    private static void BadInput(XprToken token, XprVal prevVal)
    {
        throw new ArgumentException(string.Format("Bad input, token={}, prevVal={}", token, prevVal));
    }
}