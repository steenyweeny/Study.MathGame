// Math Game 

using System.Runtime.CompilerServices;

// TODO add closing game menu to to display game history display and option to play again
// add menu user to select the number of questions they want to answer and option to have random symbols or select a type

// create unique random number generator for the game
Random nuberGen = new();
// play again loop
bool playAgain = true;

while (playAgain)
{
    List<(int number1, string symbol, int number2, int answer, string userAnswer, bool userCorrect)> questionsHistory = new();
    int questionsAsked = 0;
    int correctAnswers = 0;
    string questionsRequested = string.Empty;
    string playAgainInput = string.Empty;

    Console.WriteLine("Welcome to the Math Game!\nHow many questions would you like to answer??");
    questionsRequested = Console.ReadLine();
    if (!int.TryParse(questionsRequested, out int questionsRequestedInt) || questionsRequestedInt <= 0)
    {
        Console.WriteLine("Please enter a positive number.");
        break;
    }
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
    } while (questionsAsked < questionsRequestedInt && playAgain == true);

    Console.WriteLine($"\tGame Over! You answered {correctAnswers} correctly\nWould you like to play again??\nPlease enter Y or N");
   
    while (playAgainInput == string.Empty)
    {
        playAgainInput = Console.ReadLine();
        if (playAgainInput.ToUpper() == "Y")
        {
            playAgain = true;
            break;
        }
        if (playAgainInput.ToUpper() == "N")
        {
            playAgain = false;
            break;
        }
        else 
        {
            playAgainInput = string.Empty;
            Console.WriteLine("Please enter Y or N");
        }

    }
    
        



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



