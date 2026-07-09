
using System;
using System.Reflection.Emit;

public static class GameHelpers
{
    // random symbol generator function to select a symbol for the math equation
    public static string GenerateSymbol()
    {
        Random symbolGen = new Random();
        int symbolSelected = symbolGen.Next(1, 5);

        switch (symbolSelected)
        {
            case 1: return "+";
            case 2: return "-";
            case 3: return "*";
            case 4: return "/";
            default: return "+";
        }
    }

    // function to check the answer based on the symbol and return the correct answer
    public static int checkAnswer(int num1, int num2, char sym)
    {
        switch (sym)
        {
            case '+':
                return num1 + num2;
            case '-':
                return num1 - num2;
            case '*':
                return num1 * num2;
            case '/':
                return num1 / num2;
            default:
                throw new InvalidOperationException("Invalid symbol");
        }
    }

    //public static int DifficultyLevel(int difficulty)
    //{
    //    
    //}

}