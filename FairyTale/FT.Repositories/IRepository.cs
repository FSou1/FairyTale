using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using FT.Components.Extension;
using FT.Entities;
using FT.Entities.Contract;
using FT.Repositories.NHibernate;
using NHibernate;
using NHibernate.Linq;

namespace FT.Repositories {
    public class Repository<T> : IRepository<T> where T : IEntity {
        public Repository(IUnitOfWork unitOfWork) {
            _nHibernateUnitOfWork = (NHibernateUnitOfWork)unitOfWork;
        }

        protected ISession Session => _nHibernateUnitOfWork.Session;

        public async Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> filter, int skip, int take) {
            var ids = await GetIds(filter, skip, take);
            var entities = Session.Query<T>().Where(x => ids.Contains(x.Id)).ToList();
            return entities;
        }

        public async Task<IList<T>> GetAllAsync<TKey>(Expression<Func<T, bool>> filter, Expression<Func<T, TKey>> orderBy, bool asc, int skip, int take) {
            var ids = await GetIds(filter, orderBy, asc, skip, take);
            var entities = Session.Query<T>().Where(x => ids.Contains(x.Id)).OrderBy(orderBy, asc).ToList();
            return entities;
        }

        public Task<IList<T>> GetAllAsync<TKey>(Func<T, TKey> orderBy, bool asc) {
            var entities = (asc 
                ? Session.Query<T>().OrderBy(orderBy) 
                : Session.Query<T>().OrderByDescending(orderBy)
            ).ToList();
            return Task.FromResult<IList<T>>(entities);
        }

        public Task<T> GetAsync(object id) {
            var entity = Session.Get<T>(id);
            return Task.FromResult(entity);
        }

        public Task<int> CountAsync() {
            var count = Session.Query<T>().Count();
            return Task.FromResult(count);
        }

        public Task<int> CountAsync(Expression<Func<T, bool>> filter) {
            var count = Session.Query<T>().Where(filter).Count();
            return Task.FromResult(count);
        }

        public Task UpdateAsync(T entity) {
            Session.Update(entity);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Increase performance methods
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        private Task<IList<int>> GetIds(Expression<Func<T, bool>> filter) {
            var ids = Session.Query<T>().Where(filter).Select(x => x.Id).ToList();
            return Task.FromResult<IList<int>>(ids);
        }

        /// <summary>
        /// Increase performance methods
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        private Task<IList<int>> GetIds(Expression<Func<T, bool>> filter, int skip, int take) {
            var ids = Session.Query<T>().Where(filter).Select(x => x.Id).Skip(skip).Take(take).ToList();
            return Task.FromResult<IList<int>>(ids);
        }

        /// <summary>
        /// Increase performance methods
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="filter"></param>
        /// <param name="orderBy"></param>
        /// <param name="asc"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        private Task<IList<int>> GetIds<TKey>(Expression<Func<T, bool>> filter, Expression<Func<T, TKey>> orderBy, bool asc, int skip, int take) {
            var ids = Session.Query<T>().Where(filter).OrderBy(orderBy, asc).Select(x => x.Id).Skip(skip).Take(take).ToList();
            return Task.FromResult<IList<int>>(ids);
        }

        private readonly NHibernateUnitOfWork _nHibernateUnitOfWork;
    }

    public interface IRepository<T> {
        Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> filter, int skip, int take);

        Task<IList<T>> GetAllAsync<TKey>(Expression<Func<T, bool>> filter, Expression<Func<T, TKey>> orderBy, bool asc, int skip, int take);

        Task<IList<T>> GetAllAsync<TKey>(Func<T, TKey> orderBy, bool asc);

        Task<T> GetAsync(object id);

        Task<int> CountAsync();

        Task<int> CountAsync(Expression<Func<T, bool>> filter);

        Task UpdateAsync(T entity);
    }
}
