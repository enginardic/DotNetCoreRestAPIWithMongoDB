namespace Data
{
    public abstract class Repository<TEntity, TIdentity> : IRepository<TEntity, TIdentity>
        where TEntity : Entity<TIdentity>
    {
        public abstract TEntity Insert(TEntity entity);
        
        public abstract void Update(TEntity entity);
        
        public abstract bool Delete(string id);

        public abstract TEntity Get(string id);
    }
}
