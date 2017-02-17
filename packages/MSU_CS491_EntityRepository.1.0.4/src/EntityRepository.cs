using DAL.Models.DB;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ZombieBroccoli.Models;

namespace DAL
{

    /// <summary>
    /// all entities in the database can be accessed using this repository pattern
    /// </summary>
    /// <typeparam name="T">the db object</typeparam>
    public class EntityRepository<T> where T : class, IEntity, new()
    {
        readonly DbContext _entitiesContext;

        /// <summary>
        /// overloaded constructor
        /// </summary>
        /// <param name="entitiesContext">connection to the database</param>
        public EntityRepository(DbContext entitiesContext)
        {
            if (entitiesContext == null)
            {
                throw new ArgumentNullException(nameof(entitiesContext));
            }

            _entitiesContext = entitiesContext;
        }

        /// <summary>
        /// returns all items
        /// </summary>
        /// <returns></returns>
        public async virtual Task<IQueryable<T>> GetAll()
        {
            var results = await _entitiesContext.Set<T>()
                .ToListAsync().ConfigureAwait(false);

            return results.AsQueryable();
        }

        /// <summary>
        /// gets all items that match the given expression
        /// </summary>
        /// <param name="predicate">what to filter the items by</param>
        /// <returns>a list of items</returns>
        public async virtual Task<List<T>> GetAllWhere(Expression<Func<T, bool>> predicate)
        {
            var results = await _entitiesContext.Set<T>()
                .Where(predicate)
                .ToListAsync().ConfigureAwait(false);

            return results;
        }

        /// <summary>
        /// gets all items that match the given expression
        /// </summary>
        /// <param name="predicate">what to filter the items by</param>
        /// <param name="includeProperties">a list of child properties on the object to expand</param>
        /// <returns>a list of items</returns>
        public async virtual Task<List<T>> GetAllWhere(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            var set = _entitiesContext.Set<T>().AsQueryable();

            foreach (var includeProperty in includeProperties)
            {
                set = set.Include(includeProperty);
            }

            var results = await set.Where(predicate).ToListAsync().ConfigureAwait(false);

            return results;
        }

        /// <summary>
        /// gets all values for a single property for items that match the given expression
        /// </summary>
        /// <param name="predicate">what to filter the items by</param>
        /// <param name="selectProperty">a list of child properties on the object to expand</param>
        /// <returns>a list of items</returns>
        public async virtual Task<List<R>> GetPropertyWhere<R>(Expression<Func<T, bool>> predicate, Expression<Func<T, R>> selectProperty)
        {
            var set = _entitiesContext.Set<T>().AsQueryable();

            var results = await set
                .Where(predicate)
                .Select(selectProperty)
                .ToListAsync()
                .ConfigureAwait(false);

            return results;
        }

        /// <summary>
        /// gets all items that match the given expression
        /// </summary>
        /// <param name="predicate">what to filter the items by</param>
        /// <returns>a queryable list of items</returns>
        public async virtual Task<IQueryable<T>> FindBy(Expression<Func<T, bool>> predicate)
        {
            var results = await _entitiesContext.Set<T>()
                .Where(predicate)
                .ToListAsync().ConfigureAwait(false);

            return results.AsQueryable();
        }

        /// <summary>
        /// get all objects and include the given child objects
        /// </summary>
        /// <param name="includeProperties">the properties to expand</param>
        /// <returns>all objects</returns>
        public async virtual Task<IQueryable<T>> AllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = await GetAll().ConfigureAwait(false);

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return query;
        }

        /// <summary>
        /// get a single element given a limiting expression
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async virtual Task<T> GetSingleOrDefaultWhere(Expression<Func<T, bool>> predicate)
        {
            var results = await _entitiesContext.Set<T>()
                .Where(predicate)
                .FirstOrDefaultAsync().ConfigureAwait(false);

            return results;
        }

        /// <summary>
        /// count all items matching the given limiting expression
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async virtual Task<int> Count(Expression<Func<T, bool>> predicate)
        {
            if (predicate == null)
            {
                return await Count();
            }
            else
            {
                return await _entitiesContext.Set<T>()
                    .Where(predicate)
                    .CountAsync().ConfigureAwait(false);
            }
        }

        /// <summary>
        /// count all items in a set
        /// </summary>
        /// <returns></returns>
        public async virtual Task<int> Count()
        {
            return await _entitiesContext.Set<T>()
                .CountAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// count distinct items matching the given limiting expression
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async virtual Task<int> CountDistinct<TKey>(Expression<Func<T, bool>> predicate, Func<T, TKey> keySelector)
        {
            return _entitiesContext.Set<T>()
                .Where(predicate)
                .Select(keySelector)
                .Distinct()
                .Count();
        }

        /// <summary>
        /// get a paginated list of items
        /// </summary>
        /// <typeparam name="TKey">data type of the primary key</typeparam>
        /// <param name="pageIndex">the page number to start on</param>
        /// <param name="pageSize">the number of items per page</param>
        /// <param name="keySelector">what to sort the items by</param>
        /// <returns>a paginated list of results</returns>
        public async virtual Task<PaginatedList<T>> Paginate<TKey>(int pageIndex, int pageSize, Expression<Func<T, TKey>> keySelector)
        {
            return await Paginate(pageIndex, pageSize, keySelector, null).ConfigureAwait(false);
        }

        /// <summary>
        /// get a paginated list of items
        /// </summary>
        /// <typeparam name="TKey">data type of the primary key</typeparam>
        /// <param name="pageIndex">the page number to start on</param>
        /// <param name="pageSize">the number of items per page</param>
        /// <param name="keySelector">what to sort the items by</param>
        /// <param name="predicate">limit the results to the given expression</param>
        /// <param name="reverse">set to true to get the lits of results in the opposite order</param>
        /// <param name="includeProperties">expand the given properties</param>
        /// <returns>a paginated list of results</returns>
        public async virtual Task<PaginatedList<T>> Paginate<TKey>(
            int pageIndex,
            int pageSize,
            Expression<Func<T, TKey>> keySelector,
            Expression<Func<T, bool>> predicate,
            bool reverse = false,
            params Expression<Func<T, object>>[] includeProperties)
        {
            if (pageIndex <= 0)
            {
                pageIndex = 1;
            }

            IQueryable<T> query = CreateQuery<TKey>(keySelector, predicate, reverse, includeProperties, pageSize, pageIndex);

            await query.LoadAsync().ConfigureAwait(false);

            //fire off a second query to get the total count given the same limiting filter
            int count = await Count(predicate).ConfigureAwait(false);

            return query.ToPaginatedList(pageIndex, pageSize, count);
        }

        /// <summary>
        /// add an item to the database
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Add(T entity)
        {
            _entitiesContext.Set<T>().Add(entity);
        }

        /// <summary>
        /// add an item to the database
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Add(ICollection<T> entity)
        {
            _entitiesContext.Set<T>().AddRange(entity);
        }

        /// <summary>
        /// mark an item in the database as having been modified
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Edit(T entity)
        {
            DbEntityEntry dbEntityEntry = _entitiesContext.Entry<T>(entity);
            dbEntityEntry.State = EntityState.Modified;
        }

        /// <summary>
        /// mark an item in the database as having been modified
        /// </summary>
        /// <param name="entity"></param>
        public virtual void SubObjectEdit(T entity,
            params Expression<Func<T, object>>[] includeProperties)
        {
            DbSet<T> dbEntries = _entitiesContext.Set<T>();
            dbEntries.Attach(entity);
            var obj = _entitiesContext.Entry(entity);

            foreach (var prop in includeProperties)
            {
                obj.Property(prop).IsModified = true;
            }
        }

        /// <summary>
        /// remove the association with the database so that another process can update the record safely
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Detach(T entity)
        {
            _entitiesContext.Entry(entity).State = EntityState.Detached;
        }

        /// <summary>
        /// remove the association with the database so that another process can update the record safely
        /// </summary>
        /// <param name="entity"></param>
        public virtual bool IsAttached(T entity)
        {
            var local = _entitiesContext.Set<T>().Local.FirstOrDefault(o => o == entity);
            return (local != null);
        }

        /// <summary>
        /// remove an item from the database
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Delete(T entity)
        {
            DbEntityEntry dbEntityEntry = _entitiesContext.Entry<T>(entity);
            dbEntityEntry.State = EntityState.Deleted;
        }

        /// <summary>
        /// removes all matching items from the database
        /// </summary>
        /// <param name="predicate"></param>
        public virtual void DeleteAllWhere(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> purge = _entitiesContext.Set<T>().Where(predicate);

            _entitiesContext.Set<T>().RemoveRange(purge);
        }

        /// <summary>
        /// removes all matching items from the database
        /// </summary>
        /// <param name="purge">data to remove</param>
        public virtual void DeleteAll(IEnumerable<T> purge)
        {
            _entitiesContext.Set<T>().RemoveRange(purge);
        }

        /// <summary>
        /// count the number of subobjects associated with the primary object
        /// </summary>
        /// <typeparam name="J"></typeparam>
        /// <param name="entity"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        public virtual int CountProperty<J>(T entity, Expression<Func<T, ICollection<J>>> property) where J : class, IEntity
        {
            //http://blogs.msdn.com/b/adonet/archive/2011/01/31/using-dbcontext-in-ef-feature-ctp5-part-6-loading-related-entities.aspx
            return _entitiesContext.Entry(entity)
                .Collection(property)
                .Query()
                .Count();
        }

        /// <summary>
        /// delete all specified child records before deleting the parent
        /// </summary>
        /// <typeparam name="J"></typeparam>
        /// <param name="entity">the primary object to delete</param>
        /// <param name="properties">properties that need to be expanded and deleted</param>
        public virtual void CascadeDelete<J>(T entity, params Expression<Func<T, ICollection<J>>>[] properties) where J : class, IEntity
        {
            foreach (var property in properties)
            {
                _entitiesContext.Entry(entity).Collection(property).CurrentValue.Clear();
            }

            Delete(entity);
        }

        /// <summary>
        /// commit all changes in the database
        /// </summary>
        /// <returns>the number of items updated</returns>
        public async virtual Task<int> Save()
        {
            return await _entitiesContext.SaveChangesAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// converts a set of boolean expressions into a boolean function that can be passed along with a query
        /// </summary>
        /// <param name="where">a set of expressions to be AND'd together</param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> WhereAll(params Expression<Func<T, bool>>[] where)
        {
            Expression<Func<T, bool>> output = null;

            foreach (Expression<Func<T, bool>> item in where)
            {
                if (item != null)
                {
                    if (output == null)
                    {
                        output = item;
                    }
                    else
                    {
                        output = output.AndAlso<T>(item);
                    }
                }
            }

            return output;

        }


        #region private
        private IQueryable<T> CreateQuery<TKey>(
            Expression<Func<T, TKey>> keySelector,
            Expression<Func<T, bool>> predicate,
            bool reverse,
            Expression<Func<T, object>>[] includeProperties,
            int? limit = null,
            int? page = null)
        {
            IQueryable<T> query = _entitiesContext.Set<T>().AsQueryable();

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (reverse)
            {
                query = query.OrderByDescending(keySelector);
            }
            else
            {
                query = query.OrderBy(keySelector);
            }


            if (limit > 0)
            {
                if (page > 0)
                {
                    query = query.Skip((page.Value - 1) * limit.Value);
                }

                query = query.Take(limit.Value);
            }

            return query;
        }
    }

    /// <summary>
    /// extensions for the entity repository
    /// </summary>
    public static class EntityRepositoryExtensions
    {

        /// <summary>
        /// group two expressions together using an AND
        /// </summary>
        /// <typeparam name="T">input type of both expressions</typeparam>
        /// <param name="expr1">left hand side</param>
        /// <param name="expr2">right hand side</param>
        /// <returns>left && right</returns>
        public static Expression<Func<T, bool>> AndAlso<T>(
        this Expression<Func<T, bool>> expr1,
        Expression<Func<T, bool>> expr2)
        {
            var parameter = Expression.Parameter(typeof(T));

            var leftVisitor = new ReplaceExpressionVisitor(expr1.Parameters[0], parameter);
            var left = leftVisitor.Visit(expr1.Body);

            var rightVisitor = new ReplaceExpressionVisitor(expr2.Parameters[0], parameter);
            var right = rightVisitor.Visit(expr2.Body);

            return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(left, right), parameter);
        }

        #region private
        private class ReplaceExpressionVisitor : ExpressionVisitor
        {
            private readonly Expression _oldValue;
            private readonly Expression _newValue;

            public ReplaceExpressionVisitor(Expression oldValue, Expression newValue)
            {
                _oldValue = oldValue;
                _newValue = newValue;
            }

            public override Expression Visit(Expression node)
            {
                if (node == _oldValue)
                {
                    return _newValue;
                }

                return base.Visit(node);
            }
        }
        #endregion
        #endregion
    }
}