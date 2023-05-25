namespace Xpr.xpr.Val;

internal class XprValFuncN : XprValFunc
{
    public readonly LinkedList<XprVal?> _args = new();
    
    private readonly LinkedList<float> _vals = new();

    /**
     * evaluator (retrieved from context)
     */
    private Func<ICollection<float>, float>? func;

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

        func ??= ctx.ResolveFunc(Name);
        var result = func.Invoke(_vals);
        return result;
    }

    public void AddArg(XprVal? arg)
    {
        Assert(arg != null);
        _args.AddLast(arg);
    }
}
