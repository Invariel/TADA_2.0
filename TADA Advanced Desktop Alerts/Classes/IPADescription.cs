using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TADA_Advanced_Desktop_Alerts.Classes
{
    public class IPADescription
    {
        public string Word { get; set; }
        public string Pronunciation { get; set; }

        public IPADescription(string word, string pronunciation)
        {
            Word = word;
            Pronunciation = pronunciation.Replace(" ", "");
        }
    }
}
