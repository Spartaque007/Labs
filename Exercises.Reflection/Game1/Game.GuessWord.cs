using GameInterface;
using System;
using System.IO;
using System.Text;

namespace Game1
{
    public class Game : IGame
    {
        private readonly string _guessWordFieldText = "******* Guess word*********";
        private readonly string _prevLettersFieldText = "Prev letters: ";
        private readonly string _secretWordFieldText = "Word: ";
        private readonly string _inputFieldText = "Enter letter: ";
        private readonly string _statusFieldText = " ";

        private readonly string _wordsFilePuth;
        private StringBuilder _currentWordStatus;
        private StringBuilder _prevLetters;
        private ConsoleWritter _consoleWritter;
        private string _secretWord;
        public Game()
        {
            _wordsFilePuth = @"./words.txt";
            _secretWord = GetWordsFromFile(_wordsFilePuth);
            _currentWordStatus = GetWordTemplate(_secretWord);
            _prevLetters = new StringBuilder();
            _consoleWritter = new ConsoleWritter();
            //  Console.CursorVisible = false;
        }

        public void Run()
        {
            Init();
            while (true)
            {
                var input = Console.ReadLine();
                if (input.Length == 1)
                {
                    for (int i = 0; i < _secretWord.Length; i++)
                    {
                        if(_secretWord.ToUpper()[i]==(input.ToUpper()[0]))
                        {
                            ShowStatusWarning("good");
                            _consoleWritter.Print(_secretWordFieldText, input, (i + 1) * 2);
                        }

                    }
                   
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

        private void Init()
        {
            _consoleWritter.AddField(_guessWordFieldText, 0);
            _consoleWritter.AddField(_secretWordFieldText, 2);
            _consoleWritter.Print(_secretWordFieldText, _currentWordStatus.ToString());
            _consoleWritter.AddField(_prevLettersFieldText, 4);
            _consoleWritter.AddField(_inputFieldText, 6);
            _consoleWritter.AddField(_statusFieldText, 8);
            Console.CursorTop = 10;
            Console.CursorLeft = 0;

        }

        private string GetWordsFromFile(string puth)
        {
            //string textFromFile;

            //using (StreamReader reader = File.OpenText(puth))
            //{
            //    textFromFile = reader.ReadToEnd();
            //}

            //var wordsArray = textFromFile.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            var words = new string[] { "aabababba", "bbaabcbacbb", "cacbcacbcacbc" };


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

        private void ShowStatus(string text)
        {
            _consoleWritter.Clear(_statusFieldText);
            _consoleWritter.Print(_statusFieldText, text, 0);
            Console.WriteLine();
        }
    }
}