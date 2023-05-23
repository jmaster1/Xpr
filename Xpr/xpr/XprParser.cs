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
        if(!xt.isEof()) {
            throw new Exception(string.Format("Unexpected remaining text '{}' in source expression '{}'",
                source.Substring(xt.cur), source));
        }
        return val;
    }
    
    public XprVal parseVal(XprTokenizer xt) {
        return parseVal(xt, true);
    }

    XprVal parseVal(XprTokenizer xt, bool checkMathOperations)
    {
        if (xt.isEof())
        {
            return null;
        }

        xt.nextToken();
        XprVal f = null;
        return f;
    }
}