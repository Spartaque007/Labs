using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class Word
    {

        public string TextWord { get; set; }

        public Word(string word)
        {
            TextWord = word;
        }

        public bool TryCheckLetter(char letter)
        {
            if (TextWord.ToUpper().Contains(letter.ToString().ToUpper()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool TryCheckWord(string word)
        {
            if (word.ToUpper().Equals(word.ToUpper()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}