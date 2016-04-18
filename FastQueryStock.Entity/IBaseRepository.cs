using FastQueryStock.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FastQueryStock.Entity
{
    public interface IBaseRepository<T> where T : BaseEntity, new()
    {
        /// <summary>
        /// Save domain model to database, when you want to a large model to database, you should use Add(IList<T> list) 
        /// </summary>
        /// <param name="model"></param>
        void Add(T model);

        /// <summary>
        /// Save a large of domain model to database
        /// </summary>
        /// <param name="list"></param>
        void Add(IList<T> list);

        /// <summary>
        /// Delete model by primary key
        /// </summary>
        /// <param name="id"></param>
        void Delete(string id);

        /// <summary>
        /// Delete domain model
        /// </summary>
        /// <param name="model"></param>
        void Delete(T model);

        /// <summary>
        /// Delete a large of domain model
        /// </summary>
        /// <param name="list"></param>
        void Delete(IList<T> list);

        /// <summary>
        /// Update entity model
        /// </summary>
        /// <param name="entity"></param>
        void Update(T entity);

        /// <summary>
        /// Get domain model by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetById(string id);

        /// <summary>
        /// Get all of domain model
        /// </summary>
        /// <returns></returns>
        List<T> GetAll();

        /// <summary>
        /// Get all of domain model with filter
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        List<T> GetAll(Expression<Func<T, bool>> condition = null);
    }
}
