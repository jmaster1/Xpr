using System.Text;
using Xpr.xpr.Math;
using static Xpr.xpr.Token.XprTokenType;

namespace Xpr.xpr.Token;

/**
 * converts character stream of expression into xpr tokens
 */
public class XprTokenizer
{
    private const char DecimalSeparator = '.';
    private const char ArgSeparator = ',';
    private const char Underscore = '_';
    private const char BracketOpen = '(';
    private const char BracketClose = ')';
    private const int Eof = -1;
    
    /**
     * source string
     */
    public readonly string Src;
    public readonly int Len;
    
    /**
     * cursor on the string
     */
    public int Cur;
    
    private readonly StringBuilder _sb = new();
    
    public bool IsEof => Cur == Len;

    /**
     * bracket counter, increment on open, decrement on close
     */
    private int _bracketStack = 0;

    public XprTokenizer(string src)
    {
        Src = src;
        Cur = 0;
        Len = src.Length;
    }

    /**
     * skip all the whitespaces starting from current position
     * @return true if eof
     */
    private bool SkipWhitespaces()
    {
        for (var c = Peek(); c != Eof && char.IsWhiteSpace((char)c); c = Peek())
        {
            Cur++;
        }

        return IsEof;
    }

    private int Peek()
    {
        return IsEof ? Eof : Src[Cur];
    }
    
    public XprToken? nextToken()
    {
        if (SkipWhitespaces())
        {
            return null;
        }
        //
        // resolve token type from 1st char
        var n = Peek();
        var c = (char)n;
        var tokenType = Invalid;
        object? tokenValue = null;
        var range = new SrcRange(Src, Cur);
        if (IsNumeric(c))
        {
            tokenType = Number;
            tokenValue = ReadNumber();
        } else if (MathOperatorEx.resolve(c, out var op))
        {
            tokenType = Operator;
            tokenValue = op;
            Cur++;
        } else if (IsVariable(c))
        {
            tokenType = Variable;
            tokenValue = ReadVariable();
        } else switch (c)
        {
            case BracketOpen:
                tokenType = XprTokenType.BracketOpen;
                Cur++;
                _bracketStack++;
                break;
            case BracketClose:
                tokenType = XprTokenType.BracketClose;
                Cur++;
                if (--_bracketStack < 0)
                {
                    throw new XprParseException($"Unexpected '{BracketClose}' at {Cur - 1}");
                };
                break;
            case ArgSeparator:
                tokenType = XprTokenType.ArgSeparator;
                Cur++;
                break;
            default:
                throw new Exception("Unexpected char: " + c);
        }
        return new XprToken(tokenType, tokenValue, range.SetTo(Cur));
    }

    private string ReadVariable()
    {
        return Read(IsVariable);
    }

    private static bool IsVariable(char c)
    {
        return char.IsLetter(c) || c == Underscore;
    }

    private float ReadNumber()
    {
        var str = Read(IsNumeric);
        return float.Parse(str);
    }

    private static bool IsNumeric(char c)
    {
        return char.IsDigit(c) || c == DecimalSeparator;
    }

    private string Read(Func<char, bool> filter)
    {
        _sb.Clear();
        for (var c = Peek(); c != Eof && filter((char)c); c = Peek())
        {
            _sb.Append((char)c);
            Cur++;
        }

        return _sb.ToString();
    }

    public List<XprToken> Parse()
    {
        var list = new List<XprToken>();
        for (var token = nextToken(); token != null; token = nextToken())
        {
            list.Add(token);
        }
        return list;
    }
}