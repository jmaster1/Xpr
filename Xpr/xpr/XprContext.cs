namespace Xpr.xpr;

public class XprContext
{
    public static readonly XprContext DefaultContext = new ();
    
    public float GetVariableValue(string? name)
    {
        throw new NotImplementedException();
    }
}