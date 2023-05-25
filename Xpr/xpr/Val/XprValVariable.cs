namespace Xpr.xpr.Val;

internal class XprValVariable : XprVal
{
    public readonly string Name;
    
    public XprValVariable(string name)
    {
        Name = name;
    }

    public override XprValType GetValType()
    {
        return XprValType.Variable;
    }

    public override float Eval(XprContext ctx)
    {
        return ctx.GetVariableValue(Name);
    }
    
    public override string ToString()
    {
        return Name;
    }
}