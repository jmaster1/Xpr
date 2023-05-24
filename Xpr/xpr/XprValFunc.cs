namespace Xpr.xpr;

internal class XprValFunc : XprVal
{
    private readonly LinkedList<XprVal> _args = new();
    
    private readonly LinkedList<float> _vals = new();

    private Func<ICollection<float>, float>? func;

    private readonly XprToken? name;
    private readonly XprToken bracketOpen;
    private XprToken bracketClose;

    public XprValFunc(XprToken? operatorToken)
    {
        var mathOp = operatorToken.MathOperatorValue;
        var mathFunc = mathOp.GetMathFunc();
        func = mathFunc.GetFunc();
        func = MathFuncEx.add;
    }

    public XprValFunc(XprToken? name, XprToken bracketOpen)
    {
        this.name = name;
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

        var result = func.Invoke(_vals);
        return result;
    }

    public override XprVal consume(XprVal val)
    {
        /*
        if (left == null)
        {
            left = val;
        } else if (right == null)
        {
            right = val;
        }
        else
        {
            throw new ArgumentOutOfRangeException();
        }
        */

        return default;
    }

    public void Close(XprToken token)
    {
        bracketClose = token;
    }
}