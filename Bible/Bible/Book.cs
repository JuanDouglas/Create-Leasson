using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Bible
{
    public class Book : IEquatable<Book>
    {
        public string Name { get; }
        public List<Chapter> Chapters { get; }

        public Book(string name, List<Chapter> chapters)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Chapters = chapters ?? throw new ArgumentNullException(nameof(chapters));
        }

        public Book(string name)
        {
            Name = name;
            Chapters = new List<Chapter>();
        }

        public void AddChapter(Chapter chapter) {
            Chapters.Add(chapter);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Book);
        }

        public bool Equals(Book other)
        {
            return other != null &&
                   Name == other.Name &&
                   EqualityComparer<List<Chapter>>.Default.Equals(Chapters, other.Chapters);
        }

        public static bool operator ==(Book left, Book right)
        {
            return EqualityComparer<Book>.Default.Equals(left, right);
        }

        public static bool operator !=(Book left, Book right)
        {
            return !(left == right);
        }
    }
}
