using GameInterface;
using System;
using System.Text;

namespace Game1
{
    [GameName("GuessWord")]
    public class GuessWord : IGame
    {
        private const string LettersTemplate = " _";

        private readonly string GuessWordFieldText = "******* Guess word********* (Enter \"exit\" for exit) ";
        private readonly string PrevLettersFieldText = "Prev letters: ";
        private readonly string SecretWordFieldText = "Word: ";
        private readonly string InputFieldText = "Enter letter: ";
        private readonly string StatusFieldText = " ";
        

        private StringBuilder _currentWordStatus;
        private ConsoleWritter _consoleWritter;
        private string _secretWord;


        public GuessWord()
        {
            _secretWord = GetWord();
            _currentWordStatus = GetWordTemplate(_secretWord);
            _consoleWritter = new ConsoleWritter();
        }


        public void Run()
        {
            bool inputIsValid = false;
            bool gameExit = false;
            bool goGame = false;
            string input = "";
            while (!gameExit)
            {
                while (!inputIsValid)
                {
                    Console.Clear();
                    Console.WriteLine("Enter \"Y\" to START or \"N\" to EXIT and press ENTER");
                    input = Console.ReadLine();
                    if (input.ToUpper() == "Y")
                    {
                        goGame = true;
                        inputIsValid = true;
                        _secretWord = GetWord();
                    }
                    else if (input.ToUpper() == "N")
                    {
                        gameExit = true;
                        inputIsValid = true;
                        goGame = false;
                    }
                }

                if (goGame)
                {
                    Init();
                    GamePlay();
                    inputIsValid = false;
                }
            }
        }

        private void GamePlay()
        {
            var leftLettersQuantity = _secretWord.Length;
            bool gameFinished = false;
            while (!gameFinished)
            {
                var input = Console.ReadLine().ToUpper();
                if (input.Length == 1)
                {
                    var quantityLetterInWord = GetCountLettersInWordAndPrint(input);
                    leftLettersQuantity -= quantityLetterInWord;
                    if (quantityLetterInWord == 0)
                    {
                        ShowStatusWarning("No this letter");
                    }
                    if (leftLettersQuantity == 0)
                    {
                        ShowStatusWarning($"Good!!! Press any key");
                        Console.ReadKey();
                        gameFinished = true;
                    }
                }
                else if (input == "exit")
                {
                    gameFinished = true;
                    Console.Beep();
                }
                else
                {
                    ShowStatusWarning("Error");
                }
                Console.CursorTop = 10;
                Console.CursorLeft = 0;
                Console.Write("                                          ");
                Console.CursorLeft = 0;
            }
        }

        private int GetCountLettersInWordAndPrint(string letter)
        {
            var countLetterInWord = 0;
            for (int i = 0; i < _secretWord.Length; i++)
            {
                if (_secretWord[i] == (letter[0]))
                {
                    ShowStatusWarning("good");
                    _consoleWritter.Print(SecretWordFieldText, letter, (i + 1) * 2 - 1);
                    countLetterInWord++;
                }
            }
            return countLetterInWord;
        }

        private void Init()
        {
            Console.Clear();
            Console.Beep();
            _consoleWritter.AddField(GuessWordFieldText, 0);
            _consoleWritter.AddField(SecretWordFieldText, 2);
            _consoleWritter.Print(SecretWordFieldText, _currentWordStatus.ToString());
            _consoleWritter.AddField(PrevLettersFieldText, 4);
            _consoleWritter.AddField(InputFieldText, 6);
            _consoleWritter.AddField(StatusFieldText, 8);
            Console.CursorTop = 10;
            Console.CursorLeft = 0;
        }

        private string GetWord()
        {
            var data = "red/yellow/green/blue/black/white/orange/violet";
            var words = data.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            var random = new Random();
            return words[random.Next(words.Length - 1)].ToUpper();
        }

        private StringBuilder GetWordTemplate(string word)
        {
            var wordTemplate = new StringBuilder();
            for (int i = 0; i < word.Length; i++)
            {
                wordTemplate.Append(LettersTemplate);
            }

            return wordTemplate;
        }

        private void ShowStatusWarning(string text)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Red;
            _consoleWritter.Clear(StatusFieldText);
            _consoleWritter.Print(StatusFieldText, text, 0);
            Console.WriteLine();
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}