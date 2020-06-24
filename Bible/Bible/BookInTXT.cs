using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Bible
{
    internal class BookInTXT
    {
        public string Name { get; }
        List<ChapterInTXT> Chapters { get; }

        public BookInTXT(string name, List<ChapterInTXT> chapters)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            this.Chapters = chapters ?? throw new ArgumentNullException(nameof(chapters));
        }

        public BookInTXT(string name)
        {
            Name = name;
            Chapters = new List<ChapterInTXT>();
        }
        public void AddChapert(ChapterInTXT chapter) {
            Chapters.Add(chapter);
        }
        public Book GetBook() {
            Book bookResult = new Book(Name);
            foreach (var item in Chapters)
            {
                bookResult.AddChapter(item.GetChapter());
            }
            return bookResult;
        }
    }
}
