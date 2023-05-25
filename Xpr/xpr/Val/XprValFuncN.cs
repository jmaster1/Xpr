namespace Xpr.xpr.Val;

internal class XprValFuncN : XprValFunc
{
    private readonly LinkedList<XprVal?> _args = new();
    
    private readonly LinkedList<float> _vals = new();

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
            _vals.AddLast(val);
        }

        _func ??= ctx.ResolveFunc(Name);
        var result = _func.Invoke(_vals);
        return result;
    }

    public void AddArg(XprVal? arg)
    {
        Assert(arg != null);
        _args.AddLast(arg);
    }

    public XprVal? Reduce()
    {
        if (_args.Count == 1)
        {
            return new XprValFunc1(Name)
            {
                arg = _args.First.Value
            };
        }
        if (_args.Count == 2)
        {
            return new XprValFunc2(Name)
            {
                arg1 = _args.First.Value,
                arg2 = _args.Last.Value
            };
        }
        return this;
    }
}
