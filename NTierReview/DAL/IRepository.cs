namespace NTierReview.DAL
{
    public interface IRepository<T> where T : class
    {
        // CREATE
        void Add(T entity);

        // READ
        T Get(int id);
        T Get(Func<T, bool> predicate);
        ICollection<T> GetAll();
        ICollection<T> GetAllWhere(Func<T, bool> predicate);

        // Update
        void Update(T entity);

        // Delete
        void Delete(T entity);


        // save
        void Save();
    }
}
