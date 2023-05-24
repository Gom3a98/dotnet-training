namespace BookStore.Models.Repositories
{
    public class AuthorRepository : IBookStoreRepository<Author>
    {
        List<Author> authors;
        public AuthorRepository() {

            authors = new List<Author>()
            {
                new Author()
                {
                    Id = 1,Name = "Ahmed"
                },
                new Author()
                {
                    Id = 3,Name = "Gomma"
                },
                new Author()
                {
                    Id = 2,Name = "Ali"
                }
            };
            
        }
        public IList<Author> GetAll()
        {
            return authors;
        }
        public void Add(Author author)
        {
            authors.Add(author);
        }
        public void Delete(int id) { 
            Author author = GetById(id);
            authors.Remove(author);
        }
        public void Update(int id , Author author)
        {
            Author a = GetById(id);
            a.Name = author.Name;

        }
        public Author GetById(int id)
        {
            Author a = authors.SingleOrDefault(a => a.Id == id);
            return a;
        }
    }
}
