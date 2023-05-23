using System.Text;

namespace Xpr.xpr;

/**
 * converts source string into xpr tokens
 */
public class XprTokenizer
{
    
    private const char DECIMAL_SEPARATOR = '.';
    private const char UNDERSCORE = '_';
    private const char BRACKET_OPEN = '(';
    private const char BRACKET_CLOSE = ')';
    
    public readonly string src;

    public StringReader sr;
    public int cur;
    int len;
    private readonly StringBuilder sb = new StringBuilder();
    

    public XprTokenizer(string src)
    {
        this.src = src;
        cur = 0;
        len = src.Length;
    }

    public bool isEof()
    {
        return cur == len;
    }
    
    public bool isEofSkipWhitespaces()
    {
        
        SkipWhitespaces();
        return cur == len;
    }

    /**
     * skip all the whitespaces starting from current position
     * @return true if eof
     */
    private bool SkipWhitespaces()
    {
        for (var c = Peek(); c != EOF && char.IsWhiteSpace((char)c); c = Peek())
        {
            cur++;
        }

        return isEof();
    }

    private const int EOF = -1;

    private int Peek()
    {
        return isEof() ? -1 : src[cur + 1];
    }
    
    private int PeekSkipWhitespaces()
    {
        SkipWhitespaces();
        return Peek();
    }
    
    private char NextChar()
    {
        return src[cur++];
    }
    
    private char CurChar()
    {
        return src[cur];
    }

    public XprToken nextToken()
    {
        if (SkipWhitespaces())
        {
            return null;
        }
        //
        // try resolve token type from 1st char
        var c = NextChar();
        XprTokenType tokenType = XprTokenType.Invalid;
        object tokenValue = null;
        if (isNumeric(c))
        {
            tokenType = XprTokenType.Number;
            tokenValue = readNumber();
        } else if (MathOperatorEx.resolve(c, out MathOperator op))
        {
            tokenType = XprTokenType.Operator;
            tokenValue = op;
        } else if (isVariable(c))
        {
            tokenType = XprTokenType.Variable;
            tokenValue = readVariable();
            //
            // this should be function if next token is open bracket
            var next = PeekSkipWhitespaces();
            if (next == BRACKET_OPEN)
            {
                tokenType = XprTokenType.Function;
            }
        } else if (c == BRACKET_OPEN)
        {
            tokenType = XprTokenType.BracketOpen;
        } else if (c == BRACKET_CLOSE)
        {
            tokenType = XprTokenType.BracketClose;
        }
        else
        {
            throw new Exception("Unexpected char: " + c);
        }
        
        return new XprToken(tokenType, tokenValue);
    }

    private string readVariable()
    {
        return read(isVariable);
    }

    private bool isVariable(char c)
    {
        return char.IsLetter(c) || c == UNDERSCORE;
    }

    private float readNumber()
    {
        string str = read(isNumeric);
        return float.Parse(str);
    }

    private bool isNumeric(char c)
    {
        return char.IsDigit(c) || c == DECIMAL_SEPARATOR;
    }

    private string read(Func<char, bool> filter)
    {
        sb.Clear();
        for (char c = CurChar(); filter(c); c = NextChar())
        {
            sb.Append(c);
        }

        return sb.ToString();
    }
}