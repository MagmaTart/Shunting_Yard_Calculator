using System;

internal enum TokenTypes
{
    Operator,
    Number,
    variable,
    Parenthesis
}

internal enum Associativenesses
{
    Left,
    Right
}

//Define Operator Types
internal enum OperatorTypes
{ Plus, Minus, Multiply, Divide, Square, Equals, Exclamation, Modulus, ReSquare }

//Defing Paren Types : Open Paren, Close Paren
internal enum ParenthesisTypes
{ Open, Close }

//Empty super class to push to same stack
public class Token { }

internal class Operator : Token
{
    //OperatorTypes
    public OperatorTypes OperatorType { get; set; }

    public Operator(OperatorTypes operatorType)
    {
        OperatorType = operatorType;
    }

    //Precedence Getter and Setter
    public int Precedence
    {
        get
        {
            switch (this.OperatorType)
            {
                case OperatorTypes.Exclamation:     // !
                    return 5;

                case OperatorTypes.Square:          // ^
                    return 4;

                case OperatorTypes.Multiply:        // *
                case OperatorTypes.Divide:          // /
                case OperatorTypes.Modulus:         // %
                    return 3;

                case OperatorTypes.Plus:            // +
                case OperatorTypes.Minus:           // -
                    return 2;

                case OperatorTypes.Equals:          // =
                    return 1;

                default:
                    throw new Exception("Invalid Operator Type for Precedence get");
            }
        }
    }

    public Associativenesses Associativeness
    {
        get
        {
            switch (this.OperatorType)
            {
                case OperatorTypes.Equals:
                case OperatorTypes.Exclamation:
                    return Associativenesses.Right;
                case OperatorTypes.Plus:
                case OperatorTypes.Minus:
                case OperatorTypes.Multiply:
                case OperatorTypes.Divide:
                case OperatorTypes.Modulus:
                case OperatorTypes.Square:
                    return Associativenesses.Left;
                default:
                    throw new Exception("Invalid Operator Type for Associativeness get");
            }
        }
    }

    //Return Operator to String : Override ToString()
    public override string ToString()
    {
        switch (OperatorType)
        {
            case OperatorTypes.Plus: return "+";
            case OperatorTypes.Minus: return "-";
            case OperatorTypes.Multiply: return "*";
            case OperatorTypes.Divide: return "/";
            case OperatorTypes.Equals: return "=";
            case OperatorTypes.Exclamation: return "!";
            case OperatorTypes.Modulus: return "%";
            case OperatorTypes.Square: return "^";
            default: return null;
        }
    }

    //Return Operator's OperatorType
    public static OperatorTypes? GetOperatorType(string operatorValue)
    {
        switch (operatorValue)
        {
            case "+": return OperatorTypes.Plus;
            case "-": return OperatorTypes.Minus;
            case "*": return OperatorTypes.Multiply;
            case "/": return OperatorTypes.Divide;
            case "=": return OperatorTypes.Equals;
            case "!": return OperatorTypes.Exclamation;
            case "%": return OperatorTypes.Modulus;
            case "^": return OperatorTypes.Square;
            case "**": return OperatorTypes.ReSquare;
            default: return null;
        }
    }
}

internal class Parenthesis : Token
{
    public ParenthesisTypes ParenthesisType { get; set; }

    public Parenthesis(ParenthesisTypes parenthesisType)
    {
        ParenthesisType = parenthesisType;
        Console.Write(ParenthesisTypes.Open);
    }

    //Return Paren to String
    public override string ToString() { if (ParenthesisType == ParenthesisTypes.Open) return "("; else return ")"; }

    //return paren's ParenthesisType
    public static ParenthesisTypes? GetParenthesisType(string parenthesisValue)
    {
        switch (parenthesisValue)
        {
            case "(": return ParenthesisTypes.Open;
            case ")": return ParenthesisTypes.Close;
            default: return null;
        }
    }
}

internal class Numeric : Token
{
    public decimal Value { get; set; }

    public Numeric(decimal value)
    {
        Value = value;
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}