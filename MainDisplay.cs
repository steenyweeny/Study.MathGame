// Math Game 

using System.Runtime.CompilerServices;

#region Main Controls
// create unique random number generator for the game
Random nuberGen = new();

// control play again loop
bool playAgain = true;
#endregion

#region Main Game Loop
while (playAgain)
{
    //Create loop variables to track questions asked, correct answers, and store question history
    List<(int number1, string symbol, int number2, int answer, string userAnswer, bool userCorrect)> questionsHistory = new();
    int questionsAsked = 0;
    int correctAnswers = 0;
    string playAgainInput = string.Empty;
    string userSymbolChoice = string.Empty;
    int questionsRequestedInt = 0;


    // Display welcome message and prompt user for number of questions and equation type
    Console.WriteLine("Welcome to the Math Game!\nHow many questions would you like to answer??");
    while (!int.TryParse(Console.ReadLine(), out questionsRequestedInt) || questionsRequestedInt <= 0)
    {
        Console.WriteLine("Don't be silly now, enter a positive number.");
    }    
    
    while(userSymbolChoice != "+" && userSymbolChoice != "-" && userSymbolChoice != "*" && userSymbolChoice != "/" )
    {
        Console.WriteLine("\nChoose an equation type or leave blank for random equations\n\t + for addition \n\t - for subtraction \n\t * for multiplication \n\t / for division ");
        userSymbolChoice = Console.ReadLine();
        if(userSymbolChoice == String.Empty)
        {
            break;
        }
    }

    // Main game loop
    do
    {
        // Create main game variables to generate random numbers, store user input, and check answers
        int questionDisplayCount = 0;
        questionDisplayCount++;
        int number1 = nuberGen.Next(1, 10);
        int number2 = nuberGen.Next(2, 20);        
        string symbol = string.Empty;
        int systemAnswer = 0;
        bool userCorrect = false;
        string userAnswer = string.Empty;

        //Clean up console and display question number and equation type
        Console.Clear();
        Console.WriteLine("ITS MATH TIME BAY-BEEE");
        Console.WriteLine($"\nQuestion {questionDisplayCount} of {questionsRequestedInt}");

        //select equation type depedning on user input
        if (userSymbolChoice != String.Empty)
        {
            symbol = userSymbolChoice;
        }
        else
        { 
            symbol = GenerateSymbol().ToString(); 
        }

        // generate the system answer and check if the answer is valid for division, gather user input and validate answer, store question history
        systemAnswer = checkAnswer(number1, number2, symbol[0]);
        if (number1 % number2 == 0 && systemAnswer >= 0)
        {
            Console.WriteLine($"\nWhat is {number1} {symbol} {number2}?");
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

    // tidy up console then display game history and ask user if they want to play again
    Console.Clear();
    Console.WriteLine("\n***Game History***\n");
    foreach (var quest in questionsHistory)
    {
        Console.WriteLine($"{quest.number1} {quest.symbol} {quest.number2} = {quest.answer} | Your Answer: {quest.userAnswer} | Correct: {quest.userCorrect}");
    }

    Console.WriteLine($"\nGame Over! You answered {correctAnswers} out of {questionsRequestedInt} correctly\n\nWould you like to play again??\nPlease enter Y or N");
   
    while (playAgainInput == string.Empty)
    {
        playAgainInput = Console.ReadLine();
        if (playAgainInput.ToUpper() == "Y")
        {
            Console.Clear();
            playAgain = true;
            break;
        }
        if (playAgainInput.ToUpper() == "N")
        { 
            Console.WriteLine("Thank you for playing the Math Game! WELL BYE NOW!");
            playAgain = false;
            break;
        }
        else 
        {
            playAgainInput = string.Empty;
            Console.WriteLine("Please follow these difficult instructions and enter Y or N");
        }        
    } 
}
#endregion

#region Functions
// random symbol generator function to select a symbol for the math equation
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

// function to check the answer based on the symbol and return the correct answer
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
#endregion



