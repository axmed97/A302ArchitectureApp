
namespace Core.DataAccess
{
    public interface IRepositoryBase<TEntity>
    {
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
