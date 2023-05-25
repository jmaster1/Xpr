using Xpr.xpr.Token;

namespace Xpr.xpr.Val;

internal class XprValFunc : XprVal
{
    public readonly LinkedList<XprVal?> _args = new();
    
    private readonly LinkedList<float> _vals = new();

    /**
     * evaluator (retrieved from context)
     */
    private Func<ICollection<float>, float>? func;

    /**
     * bracket tokens
     */
    public XprToken? bracketClose, bracketOpen;
    
    /**
     * function name variable, null for anonymous function (brackets)
     */
    public XprValVariable? nameVal;

    /**
     * function name retrieval
     */
    public string? Name => nameVal?.Token?.StringValue;

    public bool IsNamed => Name != null;
    
    public bool IsClosed => bracketClose != null;

    public override XprValType GetValType()
    {
        return XprValType.Func;
    }

    public override float Eval(XprContext ctx)
    {
        _vals.Clear();
        foreach (var arg in _args)
        {
            var val = arg.Eval(ctx);
            _vals.AddLast(val);
        }

        func ??= ctx.ResolveFunc(Name);
        var result = func.Invoke(_vals);
        return result;
    }

    public void Close(XprToken token)
    {
        bracketClose = token;
    }
    
    public override string ToString()
    {
        return GetValType() + "=" + Name;
    }

    public void AddArg(XprVal? arg)
    {
        Assert(arg != null);
        _args.AddLast(arg);
    }
}
