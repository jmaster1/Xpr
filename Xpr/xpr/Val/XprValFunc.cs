using Xpr.xpr.Token;

namespace Xpr.xpr.Val;

internal abstract class XprValFunc : XprVal
{
    /**
     * bracket tokens
     */
    public XprToken? bracketClose;
    
    /**
     * function name retrieval
     */
    public readonly string Name;

    public bool IsNamed => Name != null;
    
    public bool IsClosed => bracketClose != null;

    protected XprValFunc(string name)
    {
        Name = name;
    }

    public override XprValType GetValType()
    {
        return XprValType.Func;
    }

    public void Close(XprToken token)
    {
        bracketClose = token;
    }
}
