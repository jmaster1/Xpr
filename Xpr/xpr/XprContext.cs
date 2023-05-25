namespace Xpr.xpr;

public class XprContext
{
    public static readonly XprContext DefaultContext = new ();
    
    public readonly Map<string, Func<float, float>
    public float GetVariableValue(string? name)
    {
        throw new NotImplementedException();
    }

    public Func<ICollection<float>, float> ResolveFunc(string? name)
    {
        return x;
    }

    private float x(ICollection<float> arg)
    {
        return -1;
    }

    public Func<float, float> ResolveFunc1(string? name)
    {
        throw new NotImplementedException();
    }
}