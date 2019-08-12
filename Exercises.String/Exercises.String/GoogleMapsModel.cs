using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Exercises.String
{
    class GoogleMapsModel
    {
        private const char _parametersSeparator = '&';
        private const char _valuesSeparator = '|';
        private const char _parameterToValueSeparator = '=';
        private const char _mapToQuerySeparator = '?';

        private Dictionary<string, char> _separators = new Dictionary<string, char>()
        {
            {"+" , ' ' },
            {"%3C", '<'},
            {"%3E", '>'},
            {"%23", '#'},
            {"%25", '%'},
            {"%7C",_valuesSeparator}
        };

        public string Protocol { get; set; }

        public string Host { get; set; }

        public string AbsolutePath { get; set; }

        public Dictionary<string, List<string>> Parameters { get; set; } = new Dictionary<string, List<string>>();


        public GoogleMapsModel(string inputUrl)
        {
            var url = new Uri(inputUrl);
            Protocol = url.Scheme;
            Host = url.Host;
            AbsolutePath = url.AbsolutePath;
            string queryString = url.Query.Substring(1);
            var urlArray = queryString.Split(_parametersSeparator);
            for (int i = 0; i < urlArray.Length; i++)
            {
                var currentParameter = GetParameterWithValues(urlArray[i]);
                Parameters.Add(currentParameter.Key, currentParameter.Value);
            }
        }


        public override string ToString()
        {
            var urlStringBuilder = new StringBuilder();
            urlStringBuilder.Append(Protocol)
                .Append("://")
                .Append(Host)
                .Append(AbsolutePath)
                .Append(_mapToQuerySeparator);
            if (Parameters.Count != 0)
            {
                for (int i = 0; i < Parameters.Count; i++)
                {
                    var e = Parameters.ElementAt(i);
                    urlStringBuilder.Append(e.Key);
                    if (e.Value != null)
                    {
                        urlStringBuilder.Append(_parameterToValueSeparator);
                        urlStringBuilder.Append(GetValuesToString(e.Value));
                    }
                    if (i < Parameters.Count - 1)
                    {
                        urlStringBuilder.Append(_parametersSeparator);
                    }
                }
            }
            return urlStringBuilder.ToString();
        }

        private KeyValuePair<string, List<string>> GetParameterWithValues(string parameterWithValues)
        {
            var values = new List<string>();
            string[] stringArray = Decode(parameterWithValues).Split(_parameterToValueSeparator);
            if (stringArray.Length == 1)
            {
                return new KeyValuePair<string, List<string>>(HttpUtility.UrlDecode(stringArray[0]), null);
            }
            else
            {
                string[] splittedValues = stringArray[1].Split(_valuesSeparator);
                for (int i = 0; i < splittedValues.Length; i++)
                {
                    values.Add(HttpUtility.UrlDecode(splittedValues[i]));
                }
                return new KeyValuePair<string, List<string>>(stringArray[0], values);
            }
        }

        private string Encode(string url)
        {
            string encodingUrl = "";
            for (int i = 0; i < _separators.Count; i++)
            {
                var e = _separators.ElementAt(i);
                encodingUrl = url.Replace(e.Value.ToString(), e.Key);
            }
            return encodingUrl;
        }

        private string Decode(string url)
        {
            string decodingUrl = null;
            for (int i = 0; i < _separators.Count; i++)
            {
                var e = _separators.ElementAt(i);
                decodingUrl = url.Replace(e.Key, e.Value.ToString());
            }
            return decodingUrl;
        }

        private string GetValuesToString(List<string> values)
        {
            if (values.Count == 1)
            {
                return Encode(values[0]);
            }
            else
            {
                StringBuilder valuesStringBuilder = new StringBuilder(values[0]);
                for (int i = 1; i < values.Count; i++)
                {
                    valuesStringBuilder.Append("|");
                    valuesStringBuilder.Append(HttpUtility.UrlEncode(values[i]));
                }
                return Encode(valuesStringBuilder.ToString());
            }
        }
    }
}