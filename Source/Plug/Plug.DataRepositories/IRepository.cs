using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Plug.DataRepositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        // RETRIEVE METHODS
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");
        
        TEntity Find(object id);

        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");

        // MODIFICATION METHODS
        void Add(TEntity entity);

        void Delete(object id);

        void Delete(TEntity entityToDelete);

        void Update(TEntity entityToUpdate);
    }
}
