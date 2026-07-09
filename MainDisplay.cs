// Math Game 

using System.Runtime.CompilerServices;

// TO DO ADD difficulty levels and a timer to the game
// control play again loop
bool playAgain = true;

#region user selection and game setup
while (playAgain)
{
    //Create loop variables to track questions asked, correct answers, and store question history
    string playAgainInput = string.Empty;
    string userSymbolChoice = string.Empty;
    char symbolSelection = ' ';
    int questionCount = 0;
    int difficultyLevel = 0;

    // Display welcome message and prompt user for number of questions and equation type
    Console.WriteLine("***Welcome to the Math Game!***");

    Console.WriteLine("\nChoose your difficulty!!\nPress the corresponding number key to select");
    Console.WriteLine("\t1. EASY \n\t\t5 equations, addition and subtraction with smaller numbers");
    Console.WriteLine("\t2. MEDIUM \n\t\t10 equations, addition, subtraction and multiplication");
    Console.WriteLine("\t3. HARD \n\t\t15 equations, addition, subtraction, multiplication and division, equations will contain 3 different factors");
    Console.WriteLine("\t4. HUMBLE \n\t\t 30 equations, addition, subtraction, multiplication and division, equations will contain 4 different factors and numbers will be bigger");
    Console.WriteLine("\t5. QUEST \n\t\t 20 total equations, 5 from each difficulty level");
    Console.WriteLine("\t6. CUSTOM \n\t\t Choose your equation type and number of questions");
    
    while (!int.TryParse(Console.ReadLine(), out difficultyLevel) || difficultyLevel <= 0 || difficultyLevel >= 6)
    {
        Console.WriteLine("Enter a valid choice from the difficulty options.");
    }

    if (difficultyLevel == 6)
    {
        while (!new[] { "+", "-", "*", "/" }.Contains(userSymbolChoice))
        {
            Console.WriteLine("\nChoose an equation type or leave blank for random equations\n\t + for addition \n\t - for subtraction \n\t * for multiplication \n\t / for division ");
            userSymbolChoice = Console.ReadLine();
            if (userSymbolChoice == String.Empty)
            {
                break;
            }
            else
            symbolSelection = userSymbolChoice[0];
        }

        Console.WriteLine("How many questions would you like to answer??");
        while (!int.TryParse(Console.ReadLine(), out questionCount) || questionCount <= 0)
        {
            Console.WriteLine("Don't be silly now, enter a positive number.");
        }
    }
    // set question count based on difficulty level if user selected a preset difficulty
    else
    {
        questionCount = GameHelpers.SetQuestionCount(difficultyLevel);
    }

    MainGameLoop(questionCount, symbolSelection, playAgain = true, difficultyLevel);
}    

void MainGameLoop(int questionCount, char userSymbolChoice,bool playAgain, int difficulty)
{
    //Create tracking variables for each game instatnce
    List<(string currentEquation, int systemAnswer, string userAnswer, bool userCorrect, string strDifficulty)> questionsHistory = new();
    int questionsAsked = 0;
    int correctAnswers = 0;
    string playAgainInput = string.Empty; 
    bool questMode = (difficulty == 5); ;

    do
    {
        // Create main game variables to generate random numbers, store user input, and check answers        
        bool userCorrect = false;
        string userAnswer = string.Empty;
        string currentEquation = string.Empty;
        int systemAnswer = 0;
        string strDifficulty = string.Empty;

        // set question difficulty for quest mode

        if (questMode)
        {
            difficulty = 5;
            difficulty = GameHelpers.QuestModeDifficulty(questionsAsked);
        }

        //Clean up console and display question number and equation type
        Console.Clear();
        Console.WriteLine("ITS MATH TIME BAY-BEEE");
        Console.WriteLine($"\nQuestion {questionsAsked + 1} of {questionCount}");

        //get Equation from helper class and display to user, then get user input and check answer

        GameHelpers.MathQuestion newQuestion = GameHelpers.GenerateQuestion(difficulty);
        currentEquation = newQuestion.expressionText;
        systemAnswer = newQuestion.correctAnswer;
        strDifficulty = newQuestion.strDifficulty;

        Console.WriteLine($"\nWhat is {currentEquation}?");
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
        questionsHistory.Add((currentEquation, systemAnswer, userAnswer, userCorrect, strDifficulty));
        Console.WriteLine("\nPress any key to continue");
        Console.ReadKey();

    } while (questionsAsked < questionCount && playAgain == true);

    // tidy up console then display game history and ask user if they want to play again
    Console.Clear();
    Console.WriteLine("\n***Game History***\n");
    foreach (var quest in questionsHistory)
    {
        Console.WriteLine($"{quest.currentEquation} | Your Answer: {quest.userAnswer} | Answer: {quest.systemAnswer} | Correct: {quest.userCorrect}| Question Difficulty: {quest.strDifficulty}");
    }

    Console.WriteLine($"\nGame Over! You answered {correctAnswers} out of {questionCount} correctly\n\nWould you like to play again??\nPlease enter Y or N");

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



