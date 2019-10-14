using GameInterface;
using System;
using System.Text;

namespace Game1
{
    [GameName("GuessWord")]
    public class GuessWord : IGame
    {
        private readonly string _guessWordFieldText = "******* Guess word********* (Enter \"exit\" for exit) ";
        private readonly string _prevLettersFieldText = "Prev letters: ";
        private readonly string _secretWordFieldText = "Word: ";
        private readonly string _inputFieldText = "Enter letter: ";
        private readonly string _statusFieldText = " ";

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
                var input = Console.ReadLine();
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
                if (_secretWord.ToUpper()[i] == (letter.ToUpper()[0]))
                {
                    ShowStatusWarning("good");
                    _consoleWritter.Print(_secretWordFieldText, letter, (i + 1) * 2 - 1);
                    countLetterInWord++;
                }
            }
            return countLetterInWord;
        }

        private void Init()
        {
            Console.Clear();
            Console.Beep();
            _consoleWritter.AddField(_guessWordFieldText, 0);
            _consoleWritter.AddField(_secretWordFieldText, 2);
            _consoleWritter.Print(_secretWordFieldText, _currentWordStatus.ToString());
            _consoleWritter.AddField(_prevLettersFieldText, 4);
            _consoleWritter.AddField(_inputFieldText, 6);
            _consoleWritter.AddField(_statusFieldText, 8);
            Console.CursorTop = 10;
            Console.CursorLeft = 0;
        }

        private string GetWord()
        {
            var data = "red/yellow/green/blue/black/white/orange/violet";
            var words = data.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            return words[new Random().Next(words.Length - 1)];
        }

        private StringBuilder GetWordTemplate(string word)
        {
            var wordTemplate = new StringBuilder();
            for (int i = 0; i < word.Length; i++)
            {
                wordTemplate.Append(" _");
            }

            return wordTemplate;
        }

        private void ShowStatusWarning(string text)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Red;
            _consoleWritter.Clear(_statusFieldText);
            _consoleWritter.Print(_statusFieldText, text, 0);
            Console.WriteLine();
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}