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
        private bool _exit;
        private IDictionary<string, Game> _games;


        public string DefaultPuth { get; set; }


        public Reflector()
        {
            _exit = false;
            DefaultPuth = ConfigurationManager.AppSettings["DefaultPuth"];
            _games = GetGames();
        }


        public void Run()
        {
            while (!_exit)
            {
                Init();
            }
        }


        private void Init()
        {
            Console.Clear();
            Console.WriteLine("LOADED GAMES :\n");
            var menu = GetMenuDictionary(_games);
            PrintMenu(menu);
            var number = 0;
            var userInputIsValid = false;
            var message = "\nPlease select game or enter \"Exit\":";
            Console.WriteLine(message);
            var linesMenu = Console.CursorTop;
            while (!userInputIsValid)
            {
                CleanConsolesLines(linesMenu, Console.CursorTop + 1);
                Console.CursorTop = linesMenu - 2;
                Console.WriteLine(message);
                var choice = Console.ReadLine();
                _exit = choice.ToUpper() == "EXIT";
                userInputIsValid = (Int32.TryParse(choice, out number) && number > 0 && number < menu.Count + 1) || _exit;

                if (!userInputIsValid)
                {
                    message = "\nINCORRECT INPUT!!!! Please select game again or enter \"Exit\": ";
                }
            }

            if (userInputIsValid && !_exit)
            {
                Console.Clear();
                RunGame(menu[number]);
            }
        }

        private IDictionary<string, Game> GetGames()
        {
            var assemblyFiles = Directory.GetFiles(DefaultPuth, "*.dll");

            return assemblyFiles
                 .Select(x => Assembly.LoadFrom(x))
                 .SelectMany(a => a.GetTypes())
                 .Where(x => typeof(IGame).IsAssignableFrom(x))
                 .Select(gt => new Game
                 {
                     Name = GetName(gt) ?? Path.GetFileNameWithoutExtension(gt.Assembly.Location),
                     PuthToAssembly = gt.Assembly.Location,
                     NameOfInstance = gt.FullName
                 })
                 .ToDictionary(t => t.Name, t => t);
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

        private IDictionary<int, string> GetMenuDictionary(IDictionary<string, Game> games)
        {
            return (IDictionary<int, string>)games
                .Select((g, i) => new KeyValuePair<int, string>(i+1, g.Value.Name))
                .ToDictionary(t=>t.Key,t=>t.Value);
        }

        private void PrintMenu(IDictionary<int,string> menu)
        {
            foreach (var item in menu)
            {
                Console.WriteLine(item.Key + ") " + item.Value);
            }
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