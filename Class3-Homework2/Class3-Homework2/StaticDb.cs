using Class3_Homework2.Models;

namespace Class3_Homework2
{
    public static class StaticDb
    {

        public static List<Book> Books = new List<Book>()
        {
             new Book(){
                Id = 1,
                Author = "Author One",
                Title = "Title One",
             },
              new Book(){
                Id = 2,
                Author = "Author Tow",
                Title = "Title Two",
             },
               new Book(){
                Id = 3,
                Author = "Author Three",
                Title = "Title Three",
             },
        };
    }
}
