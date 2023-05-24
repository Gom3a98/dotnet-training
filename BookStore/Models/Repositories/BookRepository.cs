namespace BookStore.Models.Repositories
{
    public class BookRepository : IBookStoreRepository<Book>
    {
        List<Book> books;
        IBookStoreRepository<Author> authorRepository = new AuthorRepository();
        public BookRepository() {

            books = new List<Book>()
            {
                new Book()
                {
                    Id = 1,Description = "C#", Title = "C# priciples" , Author = authorRepository.GetById(1)
                },
                new Book()
                {
                    Id = 2,Description = "C++", Title = "C++ priciples",Author = authorRepository.GetById(2)
                },
                new Book()
                {
                    Id = 3,Description = "Java", Title = "Java priciples", Author = authorRepository.GetById(3)
                }
            };
        }
        public IList<Book> GetAll()
        {
            return books;
        }
        public Book GetById(int id) {
            Book b = books.SingleOrDefault(book => book.Id == id);
            return b;
        
        }
        public void Add(Book book) {
            books.Add(book);
        }

        public void Update(int id ,Book book) {
            Book b = GetById(id);
            b.Description = book.Description;
            b.Title = book.Title;
            b.Author = book.Author;
        }
        public void Delete(int id) {
            Book book = GetById(id);
            books.Remove(book);
        }
        public void DeleteAll() {
            books.Clear();
        }
    }
}
