namespace Xpr.xpr;

public class XprParser
{
    public static readonly XprParser instance = new();
    
    public static Xpr create(string src)
    {
        return instance.parse(src);
    }
    
    public static XprVal createVal(string src)
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

    private XprVal parseVal(string source)
    {
        var xt = new XprTokenizer(source);
        
        XprVal val = parseVal(xt);
        if(!xt.IsEof) {
            throw new Exception(string.Format("Unexpected remaining text '{}' in source expression '{}'",
                source.Substring(xt.cur), source));
        }
        return val;
    }
    
    public XprVal parseVal(XprTokenizer xt) {
        return parseVal(xt, true);
    }

    private readonly Stack<XprVal> vals = new();
    
    XprVal parseVal(XprTokenizer xt, bool checkMathOperations)
    {
        if (xt.IsEof)
        {
            return null;
        }

        var token = xt.nextToken();
        XprVal val = null;
        var prev = vals.Peek();
        switch (token.Type)
        {
            case XprTokenType.Invalid:
                break;
            case XprTokenType.Number:
                val = new XprValNumber(token.NumberValue);
                break;
            case XprTokenType.BracketOpen:
                //
                // this must be function if variable is prev
                break;
            case XprTokenType.BracketClose:
                break;
            case XprTokenType.Operator:
                val = new XprValOperator(token.MathOperatorValue);
                break;
            case XprTokenType.Variable:
                break;
            case XprTokenType.Function:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        if (prev != null)
        {
            val = val.consume(prev);
            if (val != null)
            {
                vals.Pop();
            }
        }
        vals.Push(val);
        return val;
    }
}