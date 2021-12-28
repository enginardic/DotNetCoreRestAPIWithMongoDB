namespace Data
{
    public interface IRepository<TEntity, TIdentity>
        where TEntity : Entity<TIdentity>
    {
        TEntity Insert(TEntity entity);

        void Update(TEntity entity);

        bool Delete(string id);

        TEntity Get(string id);
    }
}
