using System;

namespace Exercises.String
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = "https://www.google.com/maps/dir/" +
                "?api=1&" +
                "origin=Paris,France&" +
                "destination=Cherbourg,France&" +
                "travelmode=driving&" +
                "waypoints=Versailles,France%7" +
                "CChartres,France%7" +
                "CLe+Mans,France%7" +
                "CCaen,France&" +
                "waypoint_place_ids=ChIJdUyx15R95kcRj85ZX8H8OAU%7" +
                "CChIJKzGHdEgM5EcR_OBTT3nQoEA%7CChIJG2LvQNCI4kcRKXNoAsPi1Mc%7" +
                "CChIJ06tnGbxCCkgRsfNjEQMwUsc";

            GoogleMapsModel travelWay = new GoogleMapsModel(url);
            var g = travelWay.ToString();
            Console.WriteLine(travelWay);
            Console.WriteLine(url.Equals(travelWay.ToString()));
            Console.Read();
        }
    }
}