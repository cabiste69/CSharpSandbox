using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace learning_cSharp
{
    class Book
    {
        public string title;
        public string author;
        public int pages;
        private string rating;
        public static int bookCount =0;

        public void CreatBook()
        {
            
            title = Console.ReadLine();
            author = Console.ReadLine();
            pages = Convert.ToInt32(Console.ReadLine());
            Rating = "hi";
            Console.WriteLine("creating book... ");
            bookCount++;
            Thread.Sleep(1000);
        }
        
        public string Rating {
            get { return rating; }
            set { 
                if(new[] {"G","PG","PG-13","R","NR"}.Contains(value))
                {
                    rating = value;
                }
                else
                {
                    rating = "invalid";
                }
                   
            }
        }
    }
}
