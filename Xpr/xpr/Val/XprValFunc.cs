namespace Xpr.xpr;

internal class XprValFunc : XprVal
{
    private readonly LinkedList<XprVal> _args = new();
    
    private readonly LinkedList<float> _vals = new();

    private Func<ICollection<float>, float>? func;

    private XprToken? nameToken;
    private readonly XprToken bracketOpen;
    private XprToken bracketClose;

    public string? Name => nameToken?.StringValue; 

    public XprValFunc(XprToken bracketOpen)
    {
        this.bracketOpen = bracketOpen;
    }
    
    public override XprValType GetValType()
    {
        return XprValType.Func;
    }

    public override float Eval(XprContext ctx)
    {
        foreach (var arg in _args)
        {
            var val = arg.Eval(ctx);
            _vals.AddLast(val);
        }

        if (func == null)
        {
            func = ctx.ResolveFunc(Name);
        }
        var result = func.Invoke(_vals);
        return result;
    }

    public override bool consumeLeft(XprVal val)
    {
        if (!val.Is(XprValType.Variable)) return false;
        nameToken = ((XprValVariable)val).Token;
        return true;
    }

    public override bool consumeRight(XprVal val)
    {
        _args.AddLast(val);
        return true;
    }

    public void Close(XprToken token)
    {
        bracketClose = token;
    }
    
    public override string ToString()
    {
        return GetValType() + "=" + Name;
    }
}