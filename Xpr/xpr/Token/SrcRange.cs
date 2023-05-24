namespace Xpr.xpr;

public struct SrcRange
{
    public readonly string Src;

    public int From, Length;

    public SrcRange(string src, int cur)
    {
        Src = src;
        From = cur;
    }
    
    public override string ToString()
    {
        return "" + From + ":" + Length;
    }

    public SrcRange SetTo(int cur)
    {
        Length = cur - From;
        return this;
    }
}