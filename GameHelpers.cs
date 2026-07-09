
using System;
using System.Data;


public static class GameHelpers
{
    // create a record container for the question data
    public record MathQuestion(string expressionText, int correctAnswer, string strDifficulty);

    // Generates base question data based on user input selection

    public static MathQuestion GenerateQuestion(int difficulty)
    {
        Random numberGen = new();
        int num1 = 0;
        int num2 = 0;
        int num3 = 0;
        int num4 = 0;
        int answer = 0;
        char symbol1 = ' ';
        char symbol2 = ' ';
        char symbol3 = ' ';
        string expressionText = string.Empty;
        string strDifficulty = string.Empty;
        DataTable evaluationTable = new DataTable();
        bool validEquation = false;


        switch (difficulty)
        {
            case 1: //easy mode, addition and subtraction only, numbers 1-10

                symbol1 = GenerateSymbol(difficulty);
                num1 = numberGen.Next(1, 11);
                num2 = numberGen.Next(1, 11);
                expressionText = $"{num1} {symbol1} {num2}";
                strDifficulty = "Easy";
                break;

            case 2:// medium mode, addition, subtraction,1-50, multiplication 1 -10 2 factors

                symbol1 = GenerateSymbol(difficulty);
                if (symbol1 == '*')
                {
                    num1 = numberGen.Next(1, 11);
                    num2 = numberGen.Next(1, 11);
                }
                else
                {
                    num1 = numberGen.Next(1, 51);
                    num2 = numberGen.Next(1, 51);
                }

                expressionText = $"{num1} {symbol1} {num2}";
                strDifficulty = "Medium";
                break;

            case 3: // hard difficulty addition, subtraction,1-100, multiplication/division 1 -10 3 factors

                while (!validEquation)
                {
                    symbol1 = GenerateSymbol(difficulty);
                    if (symbol1 == '*' || symbol1 == '/')
                    {
                        num1 = numberGen.Next(1, 11);
                        num2 = numberGen.Next(1, 11);

                    }
                    else
                    {
                        num1 = numberGen.Next(1, 101);
                        num2 = numberGen.Next(1, 101);
                    }

                    symbol2 = GenerateSymbol(difficulty);
                    if (symbol2 == '*' || symbol2 == '/')
                    {
                        num3 = numberGen.Next(1, 11);

                    }
                    else
                    {
                        num3 = numberGen.Next(1, 100);
                    }
                    expressionText = $"{num1} {symbol1} {num2} {symbol2} {num3}";                    
                    validEquation = IsValidEquation(expressionText);
                }
                strDifficulty = "Hard";
                break;

            case 4:// humble mode, addition, subtraction, multiplication, division , numbers 1-500 4 factors no safetyguards.

                while (!validEquation)
                {
                    symbol1 = GenerateSymbol(difficulty);
                    symbol2 = GenerateSymbol(difficulty);
                    symbol3 = GenerateSymbol(difficulty);

                    num1 = numberGen.Next(1, 501);
                    num2 = numberGen.Next(1, 501);
                    num3 = numberGen.Next(1, 501);
                    num4 = numberGen.Next(1, 501);

                    // Check for division by zero and ensure whole number results for division
                    expressionText = $"{num1} {symbol1} {num2} {symbol2} {num3} {symbol3} {num4}";
                    validEquation = IsValidEquation(expressionText);
                }
                strDifficulty = "Humble";
                break;

            default:
                throw new InvalidOperationException("Invalid difficulty level");
        }

        double result = Convert.ToDouble(evaluationTable.Compute(expressionText, string.Empty));
        answer = Convert.ToInt32(result);
        return new MathQuestion(expressionText, answer, strDifficulty);
    }

    // random symbol generator function to select a symbol for the math equation
    public static char GenerateSymbol(int difficulty)
    {
        Random symbolGen = new Random();
        int symbolSelected = symbolGen.Next(1, 5);

        switch (difficulty)
        {
            //easy mode only addition subtraction
            case 1:
                symbolSelected = symbolGen.Next(1, 3);
                if (symbolSelected == 1)
                    return '+';
                else
                    return '-';

            //medium dificulty, addition subtraction multiplication
            case 2:
                symbolSelected = symbolGen.Next(1, 4);
                if (symbolSelected == 1)
                    return '+';
                else if (symbolSelected == 2)
                    return '-';
                else
                    return '*';

            //hard/humble/random difficulty, addition subtraction multiplication division
            case 3 or 4 or 5:
                symbolSelected = symbolGen.Next(1, 5);
                if (symbolSelected == 1)
                    return '+';
                else if (symbolSelected == 2)
                    return '-';
                else if (symbolSelected == 3)
                    return '*';
                else
                    return '/';

            default: return '+';
        }
    }

    // set question count based on difficulty level if user selected a preset difficulty

    public static int SetQuestionCount(int difficulty)
    {  
        int questionCount = 0;

        if (difficulty != 6)
        {
            switch (difficulty)
            {
                case 1:
                    questionCount = 5;
                    break;
                case 2:
                    questionCount = 10;
                    break;
                case 3:
                    questionCount = 15;
                    break;
                case 4:
                    questionCount = 30;
                    break;
                case 5:
                    questionCount = 20;
                    break;
            }
        }
        return questionCount;
    }

    public static int QuestModeDifficulty(int questionNumber)
    {
        if (questionNumber <= 5)
            return 1; // Easy
        else if (questionNumber <= 10)
            return 2; // Medium
        else if (questionNumber <= 15)
            return 3; // Hard
        else
            return 4; // Humble
    }

    private static bool IsValidEquation(string expression)
    {
        try
        {
            DataTable dt = new DataTable();
            var result = dt.Compute(expression, string.Empty);
            return true;
        }
        catch
        {
            return false;
        }
    }
}




