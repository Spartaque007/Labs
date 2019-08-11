using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Exercises.String
{
    class GoogleMapsModel
    {
        public Dictionary<string, List<string>> UrlStruct { get; }=new Dictionary<string, List<string>>();


        public GoogleMapsModel(string url)
        {
            string _url = DecodeUrl(url);
            int i = 0;
            while (i < _url.Length)
            {
                StringBuilder currentValue = new StringBuilder();
                StringBuilder currentKey = null;
                List<string> currentValues = new List<string>();

                if (_url[i] == '&')
                {
                    i++;
                    currentKey = new StringBuilder();
                    while (_url[i] != '=')
                    {
                        currentKey.Append(_url[i]);
                        i++;
                    }
                    i++;
                }

                while (i < _url.Length && _url[i] != '&')
                {
                    if (_url[i] == '|')
                    {
                        currentValues.Add(currentValue.ToString());
                        currentValue.Clear();
                        i++;
                    }

                    currentValue.Append(_url[i]);
                    i++;
                }

                currentValues.Add(currentValue.ToString());

                if (currentKey == null)
                {
                    currentKey = new StringBuilder("url");
                }
                UrlStruct.Add(currentKey.ToString(), currentValues);
            }
        }

        public override string ToString()
        {
            StringBuilder url = new StringBuilder();
            for (int i = 0; i < UrlStruct.Count; i++)
            {
                var e = UrlStruct.ElementAt(i);
                if (e.Key == "url")
                {
                    url.Append($"{e.Value[0]}");
                    continue;
                }
                else
                {
                    if (e.Value.Count > 1)
                    {
                        url.Append($"&{e.Key}={e.Value[0]}");

                        for (int j = 1; j < e.Value.Count; j++)
                        {
                            url.Append($"|{e.Value[j]}");
                        }
                    }
                    else
                    {
                        url.Append($"&{e.Key}={e.Value[0]}");
                    }
                }
            }
            return EncodeUrl(url.ToString());
        }

        private string EncodeUrl(string url)
        {
            url.Replace("|", "%7C");
            url.Replace("+", " ");
            url.Replace("%25", "%");

            return url;
        }

        private string DecodeUrl(string url)
        {
            url.Replace("%7C", "|");
            url.Replace("+", " ");
            url.Replace("%2C", " ");
            url.Replace("%", "%25");

            return url;
        }
    }
}