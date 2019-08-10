using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;

namespace Exercises.String
{
    class GoogleMapsModel
    {
        private readonly char[] _separators = { '&' };

        private Dictionary<string, List<string>> urlStruct = new Dictionary<string, List<string>>();


        public GoogleMapsModel(string url)
        {
            string _url = HttpUtility.UrlDecode(url);
            int i = 0;
            while (i < _url.Length)
            {
                StringBuilder _currentValue = new StringBuilder();
                StringBuilder _currentKey = null;
                List<string> _currentValues = new List<string>();

                if (_url[i] == '&')
                {
                    i++;
                    _currentKey = new StringBuilder();
                    while (_url[i] != '=')
                    {
                        _currentKey.Append(_url[i]);
                        i++;
                    }
                    i++;
                }

                while (i < _url.Length && _url[i] != '&')
                {
                    if (_url[i] == '|')
                    {
                        _currentValues.Add(_currentValue.ToString());
                        _currentValue.Clear();
                        i++;
                    }

                    _currentValue.Append(_url[i]);
                    i++;
                }

                _currentValues.Add(_currentValue.ToString());

                if (_currentKey == null)
                {
                    _currentKey = new StringBuilder("url");
                }

                
                urlStruct.Add(_currentKey.ToString(), _currentValues);
            }
        }

        public override string ToString()
        {
            StringBuilder url = new StringBuilder();
            for (int i = 0; i < urlStruct.Count; i++)
            {
                var e = urlStruct.ElementAt(i);
                if (e.Key == "url")
                {
                    url.Append($"{e.Value[0]}&");
                    continue;
                }
                else
                {
                    if (e.Value.Count > 1)
                    {
                        url.Append($"{e.Key}={e.Value[0]}");
                        for (int j = 1; j < e.Value.Count; j++)
                        {
                            url.Append($"%7{e.Value[j]}");
                        }

                        url.Append("&");
                    }
                    else
                    {
                        url.Append($"{e.Key}={e.Value[0]}&");
                    }
                }
            }

            return  HttpUtility.UrlEncode( url.ToString(), Encoding.UTF32);
        }

        private string EncodingUrl(string url)
        {

        }

        private string DecodingUrl(string url)
        {

        }
    }
}