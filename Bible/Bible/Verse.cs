using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Bible
{
    public class Verse
    {
        public int Number { get; }
        public string Text { get; }

        public Verse(int number, string text)
        {
            Number = number;
            Text = text ?? throw new ArgumentNullException(nameof(text));
        }
    }
}
