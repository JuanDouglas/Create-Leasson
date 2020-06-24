using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Bible
{
    public class Bible
    {
        List<Book> Books { get; }
        public Bible(List<Book> books)
        {
            Books = books ?? throw new ArgumentNullException(nameof(books));
        }
        public Bible(byte[] folder)
        {
            StreamReader stream = new StreamReader(new MemoryStream(folder));
            var result = new Bible(stream.ReadToEnd());
            this.Books = result.Books;
        }
        public Bible(string value)
        {
            var result = new Bible(value.Split('\n'));
            this.Books = result.Books;
        }
        public Bible(string[] lines)
        {
            this.Books = GetBooks(lines);
        }
        private List<Book> GetBooks(string[] lines)
        {
            List<BookInTXT> bookInTXTs = new List<BookInTXT>();
            int startChapter = 0;
            bool startSeted = false;
            int endChapter = 0;
            bool endSeted = false;
            for (int i = 7; i < lines.Length; i++)
            {
                string[] value = lines[i].Split(' ');
                if (value.Length == 3)
                {
                    if (startSeted == false)
                    {
                        try
                        {
                            int test = Convert.ToInt32(value[2]);
                            startChapter = i + 1;
                            startSeted = true;
                        }
                        catch (Exception)
                        {
                        }
                    }
                    else
                    {
                        if (endSeted == false)
                        {

                            try
                            {
                                int test = Convert.ToInt32(value[2]);
                                endChapter = i - 1;
                                endSeted = true;
                            }
                            catch (Exception)
                            {
                            }

                        }
                    }
                }
                if (startSeted == true && endSeted == true)
                {
                    int leaght = endChapter - startChapter;
                    string[] chapterLines = new string[leaght];
                    int count = 0;
                    for (int j = startChapter; j < endChapter; j++)
                    {
                        chapterLines[count] = lines[j];
                        count++;
                    }
                    bool found = false;
                    int countList = 0;
                    foreach (var item in bookInTXTs)
                    {
                        if (item.Name == lines[startChapter - 1].Split(' ')[1])
                        {
                            bookInTXTs[countList].AddChapert(new ChapterInTXT(chapterLines, lines[startChapter - 1]));
                            found = true;
                        }
                        countList++;
                    }
                    if (!found)
                    {
                        var book = new BookInTXT(lines[startChapter-1].Split(' ')[1]);
                        book.AddChapert(new ChapterInTXT(chapterLines, lines[startChapter - 1]));
                        bookInTXTs.Add(book);
                    }
                    startChapter = 0;
                    startSeted = false;
                    endChapter = 0;
                    endSeted = false;
                }
            }

            List<Book> books = new List<Book>();
            foreach (var item in bookInTXTs)
            {
                books.Add(item.GetBook());
            }
            return books;
        }
    
    }
}
