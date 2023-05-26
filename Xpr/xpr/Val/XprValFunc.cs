namespace Xpr.xpr.Val;

internal abstract class XprValFunc : XprVal
{
    /**
     * function name retrieval
     */
    public readonly string Name;
    
    protected XprValFunc(string name)
    {
        Name = name;
    }

    public override XprValType GetValType()
    {
        return XprValType.Func;
    }
}
