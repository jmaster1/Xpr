namespace Xpr.xpr;

public class XprParser
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
        Xpr xpr = new Xpr(source);
        xpr.Val = parseVal(source);
        xpr.Src = source;
        return xpr;
    }

    private XprVal? parseVal(string source)
    {
        var xt = new XprTokenizer(source);
        
        XprVal? val = parseVal(xt);
        if(!xt.IsEof) {
            throw new Exception(string.Format("Unexpected remaining text '{}' in source expression '{}'",
                source.Substring(xt.Cur), source));
        }
        return val;
    }
    
    public XprVal? parseVal(XprTokenizer xt) {
        return ParseVal(xt, true);
    }

    private readonly Stack<XprVal> vals = new();
    
    private readonly Stack<XprToken?> tokens = new();
    
    XprVal? ParseVal(XprTokenizer xt, bool checkMathOperations)
    {
        var token = xt.nextToken();
        if (token == null)
        {
            return null;
        }
        var prevToken = tokens.Peek();
        XprVal? val = null;
        var prevVal = vals.Peek();
        switch (token.Type)
        {
            case XprTokenType.Number:
                val = new XprValNumber(token);
                break;
            case XprTokenType.BracketOpen:
                //
                // this must be function if variable is prev
                XprToken? name = null;
                if (prevToken != null && prevToken.Type == XprTokenType.Variable)
                {
                    name = tokens.Pop();
                }
                val = new XprValFunc(name, token);
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
                val = new XprValFunc(token);
                break;
            case XprTokenType.Variable:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        //
        // push unconsumed token or created val
        if (val == null)
        {
            tokens.Push(token);
        }
        else
        {
            vals.Push(val);
        }
        return val;
    }

    private static void BadInput(XprToken token, XprVal prevVal)
    {
        throw new ArgumentException(string.Format("Bad input, token={}, prevVal={}", token, prevVal));
    }
}