using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Bible
{
    public class Chapter 
    {
        public int Number { get; }
        public string Name { get; set; }
        private List<Verse> Verses { get; }

        public Chapter(int number, string name, List<Verse> verses)
        {
            Number = number;
            Name = name;
            Verses = verses;
        }
        public Chapter(int number, string name)
        {
            Number = number;
            Name = name;
            Verses = new List<Verse>();
        }
        public void AddVerse(Verse verse)
        {
            Verses.Add(verse);
        }
        public Verse GetVerse(int number)
        {
            return Verses.FirstOrDefault(vs => vs.Number == number);
        }
        public void RemoveVerse(int number) {
            Verses.RemoveAll(vs=>vs.Number==number);
        }
        
    }
}
