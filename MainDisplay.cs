// Math Game 

using System.Runtime.CompilerServices;

Random nuberGen = new Random(); 
int number1 = nuberGen.Next(1, 100);
int number2 = nuberGen.Next(2, 200); 
string symbol = string.Empty;
int answer = 0;
string userAnswer = string.Empty;

symbol = GenerateSymbol().ToString();
answer = checkAnswer(number1, number2, symbol[0]);

Console.WriteLine($"What is {number1} {symbol} {number2}?");
userAnswer = Console.ReadLine();

if (userAnswer == answer.ToString())
{
    Console.WriteLine("Correct!");
}
else
{
    Console.WriteLine($"Incorrect! The correct answer is {answer}.");
}

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



