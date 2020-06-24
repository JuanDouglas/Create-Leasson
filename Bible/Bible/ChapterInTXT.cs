using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Bible
{
    internal class ChapterInTXT
    {
        public string[] lines { get; set; }
        public string Head { get; set; }
        private static int Last { get; set; }
        public ChapterInTXT(string[] lines, string head)
        {
            this.lines = lines ?? throw new ArgumentNullException(nameof(lines));
            Head = head ?? throw new ArgumentNullException(nameof(head));
        }
        public Chapter GetChapter()
        {
            Chapter chapterResult = new Chapter(Convert.ToInt32(Head.Split(' ')[2]),Head.Split(' ')[1]);
            foreach (var item in lines)
            {
                StringBuilder builder = new StringBuilder();
                int number;
                try
                {
                    number = Convert.ToInt32(item.Split(' ')[0]);
                    Last = number;
                    string[] text = item.Split(' ');
                    for (int i = 1; i < text.Length; i++)
                    {
                        if (i == 1)
                        {
                            builder.Append(text[i]);
                        }
                        else
                        {
                            builder.Append(" " + text[i]);
                        }
                    }
                    Verse verse = new Verse(number, builder.ToString());
                    chapterResult.AddVerse(verse);
                }
                catch (Exception)
                {
                    number = Last;
                    builder.AppendLine();
                    string[] text = item.Split(' ');
                    for (int i = 1; i < text.Length; i++)
                    {
                        if (i == 1)
                        {
                            builder.Append(text[i]);
                        }
                        else
                        {
                            builder.Append(" " + text[i]);
                        }
                    }
                    Verse result = chapterResult.GetVerse(Last);
                    StringBuilder newBuilder = new StringBuilder();
                    newBuilder.Append(result.Text);
                    newBuilder.Append(builder.ToString());
                    chapterResult.RemoveVerse(number);
                    Verse verse = new Verse(number, newBuilder.ToString());
                    chapterResult.AddVerse(verse);
                }

            }
            return chapterResult;
        }
    }
}
