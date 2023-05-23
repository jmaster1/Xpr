namespace Xpr.xpr;

public class Xpr
{
    public string Src;

    public XprVal Val;

    public Xpr(string src)
    {
        Src = src;
    }
    
    public Xpr(string src, bool parse)
    {
        Src = src;
        if(parse) {
            Val = XprParser.createVal(src);
        }
    }
}
