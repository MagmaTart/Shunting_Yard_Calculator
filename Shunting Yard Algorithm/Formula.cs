using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class Formula
{
    public Stack<Token> InfixTokens { get; set; }
    public Stack<Token> PostfixTokens { get; set; }
    public string RawFormula { get; set; }

    public Formula(string rawFormula)
    {
        // store the raw formula
        RawFormula = rawFormula;
        InfixTokens = new Stack<Token>();
        PostfixTokens = new Stack<Token>();

        #region generate the InFix Stack

        Stack<Token> tokens = new Stack<Token>();
        string store = "";

        while (rawFormula.Length > 0)
        {
            //Get next character : Get Substring at String's first character
            string ThisChar = rawFormula.Substring(0, 1);

            if (Regex.IsMatch(ThisChar, "[0-9\\.]"))
            {
                store += ThisChar;
            }
            else if (Operator.GetOperatorType(ThisChar) != null)
            {
                if (store != "")
                {
                    tokens.Push(new Numeric(Convert.ToDecimal(store)));
                    store = "";
                }
                tokens.Push(new Operator((OperatorTypes)Operator.GetOperatorType(ThisChar)));
            }
            else if (Parenthesis.GetParenthesisType(ThisChar) != null)
            {
                if (store != "")
                {
                    tokens.Push(new Numeric(Convert.ToDecimal(store)));
                    store = "";
                }
                tokens.Push(new Parenthesis((ParenthesisTypes)Parenthesis.GetParenthesisType(ThisChar)));
            }
            else
            {
                //To Handle Blanks
                if (!(ThisChar == " " && !(store != "" && Regex.IsMatch(rawFormula.Substring(1, 1), "[0-9\\.]"))))
                {
                    throw new Exception("Invalid character in Formula: " + ThisChar);
                }
            }
            rawFormula = rawFormula.Substring(1);
        }

        // Push all left
        if (store != "")
        {
            tokens.Push(new Numeric(Convert.ToDecimal(store)));
        }

        Stack<Token> reversedStack = new Stack<Token>();
        while (tokens.Count > 0) reversedStack.Push(tokens.Pop());

        InfixTokens = reversedStack;

        #endregion generate the InFix Stack

        #region generate the PostFix Stack

        //Load Infix token stack
        Stack<Token> infixTokens = new Stack<Token>(InfixTokens);
        Stack<Token> InFixStack = new Stack<Token>();
        while (infixTokens.Count > 0) InFixStack.Push(infixTokens.Pop());
        // new stacks
        Stack<Token> output = new Stack<Token>();       //Stack to Print
        Stack<Token> operators = new Stack<Token>();

        while (InFixStack.Count > 0)
        {
            //Get new token
            Token currentToken = InFixStack.Pop();

            // If is Operator
            if (currentToken.GetType() == typeof(Operator))
            {
                //Operator stack hasn't blank and type of next pop is Operator
                while (operators.Count > 0 && operators.Peek().GetType() == typeof(Operator))
                {
                    Operator currentOperator = (Operator)currentToken;      //Current token
                    Operator nextOperator = (Operator)operators.Peek();     //Top token at Operator stack

                    //Pop
                    if ((currentOperator.Associativeness == Associativenesses.Left && currentOperator.Precedence <= nextOperator.Precedence)
                        || (currentOperator.Associativeness == Associativenesses.Right && currentOperator.Precedence < nextOperator.Precedence))
                    {
                        //to Tree
                        output.Push(operators.Pop());
                    }
                    else
                    {
                        break;
                    }
                }
                // Push current token
                operators.Push(currentToken);
            }
            else if (currentToken.GetType() == typeof(Parenthesis))
            {
                switch (((Parenthesis)currentToken).ParenthesisType)
                {
                    case ParenthesisTypes.Open:
                        operators.Push(currentToken);
                        break;
                    case ParenthesisTypes.Close:
                        while (operators.Count > 0)
                        {
                            Token nextOperator = operators.Peek();
                            if (nextOperator.GetType() == typeof(Parenthesis) && ((Parenthesis)nextOperator).ParenthesisType == ParenthesisTypes.Open)
                                break;
                            output.Push(operators.Pop());
                        }
                        operators.Pop();
                        break;
                }
            }
            else if (currentToken.GetType() == typeof(Numeric))
            {
                output.Push(currentToken);
            }
        }

        while (operators.Count > 0)
        {
            output.Push(operators.Pop());
        }

        reversedStack = new Stack<Token>();
        while (output.Count > 0) reversedStack.Push(output.Pop());

        PostfixTokens = reversedStack;

        #endregion generate the PostFix Stack
    }

    public decimal Calculate()
    {
        Stack<Numeric> EvaluationStack = new Stack<Numeric>();
        Stack<Token> postFixStack = new Stack<Token>(PostfixTokens);
        Stack<Token> PostFixStack = new Stack<Token>();
        while (postFixStack.Count > 0) PostFixStack.Push(postFixStack.Pop());

        while (PostFixStack.Count > 0)
        {
            Token currentToken = PostFixStack.Pop();

            if (currentToken.GetType() == typeof(Numeric))
            {
                EvaluationStack.Push((Numeric)currentToken);
            }
            else if (currentToken.GetType() == typeof(Operator))
            {
                Operator currentOperator = (Operator)currentToken;

                if (currentOperator.OperatorType == OperatorTypes.Plus
            || currentOperator.OperatorType == OperatorTypes.Minus
            || currentOperator.OperatorType == OperatorTypes.Multiply
            || currentOperator.OperatorType == OperatorTypes.Divide
            || currentOperator.OperatorType == OperatorTypes.Modulus
            || currentOperator.OperatorType == OperatorTypes.Square)
                {
                    decimal FirstValue = EvaluationStack.Pop().Value;
                    decimal SecondValue = EvaluationStack.Pop().Value;
                    decimal Result;

                    if (currentOperator.OperatorType == OperatorTypes.Plus)
                    {
                        Result = SecondValue + FirstValue;
                    }
                    else if (currentOperator.OperatorType == OperatorTypes.Minus)
                    {
                        Result = SecondValue - FirstValue;
                    }
                    else if (currentOperator.OperatorType == OperatorTypes.Divide)
                    {
                        Result = SecondValue / FirstValue;
                    }
                    else if (currentOperator.OperatorType == OperatorTypes.Multiply)
                    {
                        Result = SecondValue * FirstValue;
                    }
                    else if (currentOperator.OperatorType == OperatorTypes.Modulus)
                    {
                        Result = SecondValue % FirstValue;
                    }
                    else if (currentOperator.OperatorType == OperatorTypes.Square)
                    {
                        Result = (decimal)Math.Pow((double)SecondValue, (double)FirstValue);
                    }
                    else
                    {
                        throw new Exception("Unhandled operator in Formula.Calculate()");
                    }
                    EvaluationStack.Push(new Numeric(Result));
                }
            }
            else
            {
                throw new Exception("Unexpected Token type in Formula.Calculate");
            }
        }

        if (EvaluationStack.Count != 1)
        {
            throw new Exception("Unexpected number of Tokens in Formula.Calculate");
        }
        return EvaluationStack.Peek().Value;
    }
}