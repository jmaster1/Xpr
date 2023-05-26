using System.Text;
using Xpr.xpr.Math;
using Xpr.xpr.Util;
using static Xpr.xpr.Token.XprTokenType;

namespace Xpr.xpr.Token;

/**
 * converts character stream of expression into xpr tokens
 */
public class XprTokenizer : GenericEntity
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
    private readonly string _src;

    private readonly int _len;
    
    /**
     * cursor on the string
     */
    private int _cur;
    
    private readonly StringBuilder _sb = new();
    
    private bool IsEof => _cur == _len;

    public bool GetHasMoreTokens()
    {
        if (_peekedToken != null)
        {
            return true;
        }

        if (IsEof)
        {
            return false;
        }

        return PeekToken() != null;
    }

    /**
     * bracket counter, increment on open, decrement on close
     */
    private int _bracketStack;

    private XprToken? _peekedToken;

    public XprTokenizer(string src)
    {
        _src = src;
        _cur = 0;
        _len = src.Length;
    }

    /**
     * skip all the whitespaces starting from current position
     * @return true if eof
     */
    private bool SkipWhitespaces()
    {
        for (var c = Peek(); c != Eof && char.IsWhiteSpace((char)c); c = Peek())
        {
            _cur++;
        }

        return IsEof;
    }

    private int Peek()
    {
        return IsEof ? Eof : _src[_cur];
    }
    
    private int Read()
    {
        return IsEof ? Eof : _src[_cur++];
    }
    
    public XprToken? NextToken()
    {
        if (_peekedToken != null)
        {
            var ret = _peekedToken;
            _peekedToken = null;
            return ret;
        }
        if (SkipWhitespaces())
        {
            return null;
        }
        //
        // resolve token type from 1st char
        var n = Read();
        var c = (char)n;
        XprTokenType tokenType;
        object? tokenValue = null;
        var range = new SrcRange(_src, _cur);
        if (IsNumeric(c))
        {
            tokenType = Number;
            var str = Read(c, IsNumeric);
            tokenValue = float.Parse(str);
        } else if (MathOperatorEx.resolve(c, out var op))
        {
            tokenType = Operator;
            tokenValue = op;
        } else if (IsVariable(c))
        {
            tokenType = Variable;
            tokenValue = Read(c, IsVariableOrNumber);
        } else switch (c)
        {
            case BracketOpen:
                tokenType = XprTokenType.BracketOpen;
                _bracketStack++;
                break;
            case BracketClose:
                tokenType = XprTokenType.BracketClose;
                if (--_bracketStack < 0)
                {
                    throw new XprParseException($"Unexpected '{BracketClose}' at {_cur - 1}");
                }
                break;
            case ArgSeparator:
                tokenType = XprTokenType.ArgSeparator;
                break;
            default:
                throw new Exception("Unexpected char: " + c);
        }
        return new XprToken(tokenType, tokenValue, range.SetTo(_cur));
    }

    private static bool IsVariableOrNumber(char c)
    {
        return IsVariable(c) || char.IsDigit(c);
    }

    private static bool IsVariable(char c)
    {
        return char.IsLetter(c) || c == Underscore;
    }

    private static bool IsNumeric(char c)
    {
        return char.IsDigit(c) || c == DecimalSeparator;
    }

    private string Read(char first, Func<char, bool> filter)
    {
        _sb.Clear();
        _sb.Append(first);
        for (var c = Peek(); c != Eof && filter((char)c); c = Peek())
        {
            _sb.Append((char)c);
            _cur++;
        }

        return _sb.ToString();
    }

    public List<XprToken> Parse()
    {
        var list = new List<XprToken>();
        for (var token = NextToken(); token != null; token = NextToken())
        {
            list.Add(token);
        }
        return list;
    }

    public XprToken? PeekToken()
    {
        return _peekedToken ??= NextToken();
    }

    public void ConsumePeekToken(XprToken peekedToken)
    {
        Assert(_peekedToken != null);
        Assert(peekedToken == _peekedToken);
        _peekedToken = null;
    }
}