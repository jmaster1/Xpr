namespace Xpr.xpr.Val;

internal class XprValFuncN : XprValFunc
{
    private readonly List<XprVal?> _args = new();
    
    private float[]? _values;

    /**
     * evaluator (retrieved from context)
     */
    private Func<float[], float>? _func;

    public XprValFuncN(string name) : base(name)
    {
    }

    public override float Eval(XprContext ctx)
    {
        var n = _args.Count;
        _values ??= new float[n];
        for (var i = 0; i < n; i++)
        {
            _values[i] = _args[i]!.Eval(ctx);
        }
        _func ??= ctx.ResolveFuncN(Name);
        var result = _func.Invoke(_values);
        return result;
    }

    public void AddArg(XprVal? arg)
    {
        Assert(arg != null);
        _args.Add(arg);
    }

    public XprVal Reduce()
    {
        return _args.Count switch
        {
            0 => new XprValFunc0(Name),
            1 => new XprValFunc1(Name) { Arg = _args[0] },
            2 => new XprValFunc2(Name) { Arg1 = _args[0], Arg2 = _args[1] },
            _ => this
        };
    }
}
