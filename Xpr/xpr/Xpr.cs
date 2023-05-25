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
    
    public Xpr(string src, bool parse)
    {
        Src = src;
        if(parse)
        {
            Parse();
        }
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

    public string ToStringDeep()
    {
        return XprVal.ToJson(this);
    }
}
    
