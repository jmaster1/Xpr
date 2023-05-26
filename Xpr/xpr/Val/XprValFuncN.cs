namespace Xpr.xpr.Val;

internal class XprValFuncN : XprValFunc
{
    private readonly List<XprVal?> _args = new();
    
    private readonly List<float> _vals = new();

    /**
     * evaluator (retrieved from context)
     */
    private Func<ICollection<float>, float>? _func;

    public XprValFuncN(string name) : base(name)
    {
    }

    public override float Eval(XprContext ctx)
    {
        _vals.Clear();
        foreach (var arg in _args)
        {
            var val = arg.Eval(ctx);
            _vals.Add(val);
        }

        _func ??= ctx.ResolveFunc(Name);
        var result = _func.Invoke(_vals);
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
