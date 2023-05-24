namespace Xpr.xpr;

public class Xpr
{
    public string Src;

    public XprVal? Val;

    public Xpr(string src)
    {
        Src = src;
    }
    
    public Xpr(string src, bool parse)
    {
        Src = src;
        if(parse)
        {
            Parse();
        }
    }

    private void Parse()
    {
        Val = XprParser.createVal(Src);
    }

    public float Eval(XprContext ctx)
    {
        if (Val == null)
        {
            Parse();
        }
        return Val.Eval(ctx);
    }

    public float Eval()
    {
        return Eval(XprContext.DefaultContext);
    }
}
    
