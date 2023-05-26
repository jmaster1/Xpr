using Xpr.xpr.Val;

namespace Xpr.xpr;

public class Xpr
{
    public string Src;

    public XprVal? Val;

    public Xpr(string src)
    {
        Src = src;
    }
    
    public Xpr Parse()
    {
        Val = XprParser.createVal(Src);
        return this;
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
    
    public override string ToString()
    {
        return Val?.ToString();
    }
}
    
