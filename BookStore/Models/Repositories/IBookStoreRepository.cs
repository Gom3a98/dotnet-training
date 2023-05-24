namespace BookStore.Models.Repositories
{
    public interface IBookStoreRepository<IEntity>
    {
        IList<IEntity> GetAll();
        IEntity GetById(int id);
        void Add(IEntity entity);
        void Delete(int id);
        void Update(int id , IEntity entity);
    }
}
