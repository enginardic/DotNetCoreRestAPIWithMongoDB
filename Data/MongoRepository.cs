using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Data
{
    public abstract class MongoRepository<TEntity> : Repository<TEntity, ObjectId>, IRepository<TEntity, ObjectId>
        where TEntity : MongoEntity 
    {
        protected readonly IMongoDatabase database;
        protected IMongoCollection<TEntity> collection;

        public MongoRepository()
        {
            database = GetDatabase();
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            return collection.Find(filter).FirstOrDefault();
        }

        public override TEntity Get(string id)
        {
            return collection.Find(i => i.Id == new ObjectId(id)).FirstOrDefault();
        }

        public override TEntity Insert(TEntity entity)
        {
            entity.Id = ObjectId.GenerateNewId();
            collection.InsertOne(entity);
            return entity;
        }

        public bool Update(Expression<Func<TEntity, bool>> filter, params KeyValuePair<string, object>[] updateFields)
        {
            var updateDefinitions = updateFields.Select(i => Builders<TEntity>.Update.Set(i.Key, i.Value));

            //var updateDefinitions = Builders<TEntity>.Update.Set(, "");
            var update = Builders<TEntity>.Update.Combine(updateDefinitions);
            return collection.FindOneAndUpdate(filter, update, new FindOneAndUpdateOptions<TEntity> { IsUpsert = true }) is null!;
        }

        public virtual void Save(TEntity entity)
        {
            if (entity.Id == ObjectId.Empty)
                //if (string.IsNullOrEmpty(entity.Id))
                Insert(entity);
            Update(entity);
        }
        
        public override bool Delete(string id)
        {
            return collection.FindOneAndDelete(i => i.Id == new ObjectId(id)) != null;
        }

        #region Private Helper Methods  
        private IMongoDatabase GetDatabase()
        {
            var mongoUrl = MongoUrl.Create(GetConnectionString());
            var client = new MongoClient(mongoUrl);

            return client.GetDatabase(mongoUrl.DatabaseName);
        }

        private string GetConnectionString()
        {
            return ConnectionSettings.Instance.ConnectionString;
        }

        protected void SetCollection(string collectionName = null)
        {
            collection = database.GetCollection<TEntity>(collectionName ?? typeof(TEntity).Name);
        }
        #endregion
    }
}
