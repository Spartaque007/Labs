using System.Collections.Generic;
using System.Web;

namespace Exercises.String
{
    class GoogleMapsModel
    {
        private readonly char[] _separators = { '&' };

        public string UrlPath { get; set; }

        public string Origin { get; set; }

        public string Destination { get; set; }

        public string TravelMode { get; set; }

        public string Waypoint_place_id { get; set; }

        public List<string> Waypoints { get; set; } = new List<string>();


        public GoogleMapsModel(string url)
        {
            var urlArray = GetArrayFromUrl(url);
            UrlPath = urlArray[0];
            for (int i = 1; i < urlArray.Length; i++)
            {
                if (urlArray[i].Contains("origin"))
                {
                    Origin = urlArray[i].Substring(7);
                }
                else if (urlArray[i].Contains("destination"))
                {
                    Destination = urlArray[i].Substring(12);
                }
                else if (urlArray[i].Contains("waypoints"))
                {
                    char[] separators = { '|', '=' };
                    string[] _waypoints = urlArray[i].Split(separators);
                    for (int j = 1; j < _waypoints.Length; j++)
                    {
                        Waypoints.Add(_waypoints[j]);
                    }
                }
                else if (urlArray[i].Contains("travelmode"))
                {
                    TravelMode = urlArray[i].Substring(11);
                }
                else if (urlArray[i].Contains("waypoint_place_ids"))
                {
                    Waypoint_place_id = urlArray[i].Substring(19);
                }
            }
        }


        private string[] GetArrayFromUrl(string url)
        {
            string decodingUrl = HttpUtility.UrlDecode(url);
            string[] urlArray = decodingUrl.Split(_separators);
            return urlArray;
        }
    }
}