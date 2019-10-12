using GameInterface;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Reflector
{
    public class Reflector
    {
        private Dictionary<string, string> _games;

        public string DefaultPuth { get; set; }


        public Reflector()
        {
            _games = new Dictionary<string, string>();
            DefaultPuth = ConfigurationManager.AppSettings["DefaultPuth"];
            ValidateAssemblies();
        }


        public void Run()
        {
            Init();
        }

       private void Init()
        {
            Dictionary<int, string> menu;
            Console.WriteLine("LOADED GAMES :\n");
            var LinesMenu = PrintGamesAndGetCount(out menu) + 4;
            var number = 0;
            var UserInputIsValid = false;
            var message = "\nPlease select game:";

            while (!UserInputIsValid)
            {
                CleanConsolesLines(LinesMenu - 2, Console.CursorTop + 1);
                Console.CursorTop = LinesMenu - 2;
                Console.WriteLine(message);
                var choice = Console.ReadLine();
                UserInputIsValid = Int32.TryParse(choice, out number) && number >= 0 && number < menu.Count + 1;

                if (!UserInputIsValid)
                {
                    message = "\nINCORRECT INPUT!!!! Please select game again: ";
                }
            }

            if (UserInputIsValid)
            {
                Console.Clear();
                RunGame(menu[number]);
            }
        }

        private void RunGame(string gameName)
        {
            var game = Assembly.LoadFrom(_games[gameName]);
            var t = game.GetType("Game1.Game");
            IGame a = (IGame)Activator.CreateInstance(t);
            a.Run();
        }

        private void ValidateAssemblies()
        {
            var assemblies = Directory.GetFiles(DefaultPuth, "*.dll");

            foreach (var assembly in assemblies)
            {
                var game = Assembly.LoadFrom(assembly);
                PutToCollectionIfGame(game);
            }
        }

        private void PutToCollectionIfGame(Assembly game)
        {
            var types = game.GetTypes();
            foreach (var t in types)
            {
                if (typeof(IGame).IsAssignableFrom(t))
                {
                    var name = GetName(t) ?? Path.GetFileName(game.Location).Replace(".dll", " ");
                    _games.Add(name, game.Location);
                }
            }
        }

        private string GetName(Type gameType)
        {
            var attributes = gameType.GetCustomAttributes();

            foreach (var attribute in attributes)
            {
                var nameAttr = attribute as GameNameAttribute;

                if (nameAttr != null)
                {
                    return nameAttr.GameName;
                }
            }

            return null;
        }

        private void CleanConsolesLines(int from, int To)
        {
            var prevPos = Console.CursorTop;
            for (int i = from; i <= To; i++)
            {
                Console.CursorTop = i;
                Console.CursorLeft = 0;
                Console.Write("                                                        ");
            }
            Console.CursorTop = prevPos;
        }

        private int PrintGamesAndGetCount(out Dictionary<int, string> menu)
        {
            var i = 0;
            menu = new Dictionary<int, string>();

            for (i = 0; i < _games.Count; i++)
            {
                var currentGame = _games.ElementAt(i);
                Console.WriteLine(i + 1 + ") " + currentGame.Key);
                menu.Add(i + 1, currentGame.Key);
            }

            return i;
        }
    }
}