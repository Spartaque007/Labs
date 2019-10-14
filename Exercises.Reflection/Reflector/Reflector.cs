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
        private bool _closeApp;

        private Dictionary<string, Game> _games;

        public string DefaultPuth { get; set; }


        public Reflector()
        {
            _closeApp = false;
            _games = new Dictionary<string, Game>();
            DefaultPuth = ConfigurationManager.AppSettings["DefaultPuth"];
            ValidateAssemblies();
        }


        public void Run()
        {
            while (!_closeApp)
            {
                Init();
            }

        }


        private void Init()
        {
            Dictionary<int, string> menu;
            Console.WriteLine("LOADED GAMES :\n");
            var LinesMenu = PrintGamesAndGetCount(out menu) + 4;
            var number = 0;
            var userInputIsValid = false;
            var message = "\nPlease select game or enter \"Exit\":";
            while (!userInputIsValid)
            {
                CleanConsolesLines(LinesMenu - 2, Console.CursorTop + 1);
                Console.CursorTop = LinesMenu - 2;
                Console.WriteLine(message);
                var choice = Console.ReadLine();
                bool _closeApp = choice.ToUpper() == "EXIT";
                userInputIsValid = (Int32.TryParse(choice, out number) && number >= 0 && number < menu.Count + 1) || _closeApp;

                if (!userInputIsValid)
                {
                    message = "\nINCORRECT INPUT!!!! Please select game again: ";
                }
            }

            if (userInputIsValid && !_closeApp)
            {
                Console.Clear();
                RunGame(menu[number]);
            }
        }

        private void ValidateAssemblies()
        {
            var assemblies = Directory.GetFiles(DefaultPuth, "*.dll");

            foreach (var assembly in assemblies)
            {
                var assemlyWithGames = Assembly.LoadFrom(assembly);
                GetGamesFromAssembly(assemlyWithGames);
            }
        }

        private void GetGamesFromAssembly(Assembly assembly)
        {
            var types = assembly.GetTypes();
            foreach (var type in types)
            {
                if (typeof(IGame).IsAssignableFrom(type))
                {
                    var game = new Game();
                    game.Name = GetName(type) ?? Path.GetFileName(assembly.Location).Replace(".dll", " ");
                    game.PuthToAssembly = assembly.Location;
                    game.NameOfInstance = type.FullName;
                    _games.Add(game.Name, game);
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

        private void RunGame(string gameName)
        {
            var game = Assembly.LoadFrom(_games[gameName].PuthToAssembly);
            var t = game.GetType(_games[gameName].NameOfInstance);
            IGame a = (IGame)Activator.CreateInstance(t);
            a.Run();
        }
    }
}