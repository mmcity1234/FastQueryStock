using FastQueryStock.Entity.Context;
using FastQueryStock.Entity.Entity;
using FastQueryStock.Entity;
using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FastQueryStock.Entity.Instance
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity, new()
    {

        private DbSet<T> _dbSet;
        protected StockContext Context { get; set; }


        public BaseRepository(StockContext db)
        {
            Context = db;
            _dbSet = db.Set<T>();
        }

        public virtual void Add(T entity)
        {
            try
            {
                Context.Entry(entity).State = EntityState.Added;
            }
            catch (Exception dbEx)
            {
                StringBuilder str = new StringBuilder();
            
                string errMsg = str.ToString().Substring(0, str.Length - 1);
                throw new Exception(string.Format("Database operation fail, caused by: {0}", errMsg));
            }
        }

        public virtual void Add(IList<T> list)
        {
            foreach (var item in list)
            {
                Add(item);
            }
        }

        public virtual void Delete(T entity)
        {
            Context.Entry(entity).State = EntityState.Deleted;
        }
        public virtual void Delete(IList<T> list)
        {
            foreach (var item in list)
            {
                Delete(item);
            }
        }
        public virtual void Delete(string id)
        {
            var entity = _dbSet.Find(id);
            Delete(entity);
        }

        public virtual void Update(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
        }
        public virtual T GetById(string id)
        {
            return _dbSet.Find(id);
        }
        public virtual List<T> GetAll()
        {
            if (_dbSet == null)
                throw new Exception();

            // Look in the local cache first
            //var list = _dbSet.ToList();

            // If not found locally, query the database
            // because local cache may be zero, so we try to get data from databaes again
            //if (list == null || list.Count == 0)
            //    list = _dbSet.ToList();

            return _dbSet.ToList();
        }

        public abstract List<T> GetAll(Expression<Func<T, bool>> condition = null);

        /// <summary>
        /// 根據條件取得所有的資料並且將關聯屬性也一併查詢取回
        /// </summary>
        /// <param name="includes">關聯屬性的名稱陣列，可取回多個</param>
        /// <param name="condition"></param>
        /// <returns></returns>
        protected List<T> GetAllWithInclude(Expression<Func<T, bool>> condition = null)
        {
            IQueryable<T> query = _dbSet;

            //if (condition != null)
            //{
            //    query = query.Where(condition);
            //}
            //foreach (var includeProperty in includes)
            //{
            //    query = query.Include(includeProperty);
            //}
            return query.ToList();
        }

        
    }
}
