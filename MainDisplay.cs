// Math Game 

using System.Runtime.CompilerServices;

// TODO add list to track results 
// TODO add logic checking to ensure answers returned are whole numbers

Random nuberGen = new Random(); 
List <(int number1, string symbol, int number2, int answer, string userAnswer, bool userCorrect)> questionsHistory = new();
int questionsAsked = 0;
int correctAnswers = 0;

do
{
    int number1 = nuberGen.Next(1, 10);
    int number2 = nuberGen.Next(2, 20);
    string symbol = string.Empty;
    int systemAnswer = 0;
    bool userCorrect = false;
    string userAnswer = string.Empty;

    symbol = GenerateSymbol().ToString();
    systemAnswer = checkAnswer(number1, number2, symbol[0]);

    if (number1 % number2 == 0 && systemAnswer >= 0)
    {
        Console.WriteLine($"What is {number1} {symbol} {number2}?");
        userAnswer = Console.ReadLine();

        if (userAnswer == systemAnswer.ToString())
        {
            Console.WriteLine("Correct!");
            userCorrect = true;
            correctAnswers++;
        }
        else
        {
            Console.WriteLine($"Incorrect! The correct answer is {systemAnswer}.");
        }
        questionsAsked++;
        questionsHistory.Add((number1, symbol, number2, systemAnswer, userAnswer, userCorrect));
    }
} while (questionsAsked < 5);

Console.WriteLine($"\tGame Over! You answered {correctAnswers} correctly");




string GenerateSymbol()
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

int checkAnswer(int num1, int num2, char sym)
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



