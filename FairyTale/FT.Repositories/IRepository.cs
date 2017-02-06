using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using FT.Entities;
using FT.Repositories.NHibernate;
using NHibernate;
using NHibernate.Linq;

namespace FT.Repositories {
    public class Repository<T> : IRepository<T> {
        public Repository(IUnitOfWork unitOfWork) {
            _nHibernateUnitOfWork = (NHibernateUnitOfWork)unitOfWork;
        }

        protected ISession Session => _nHibernateUnitOfWork.Session;

        public Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> filter, int skip, int take) {
            var entities = Session.Query<T>().Where(filter).Skip(skip).Take(take).ToList();
            return Task.FromResult<IList<T>>(entities);
        }

        public Task<IList<T>> GetAllAsync<TKey>(Func<T, TKey> orderBy, bool asc) {
            var entities = (asc 
                ? Session.Query<T>().OrderBy(orderBy) 
                : Session.Query<T>().OrderByDescending(orderBy)
            ).ToList();
            return Task.FromResult<IList<T>>(entities);
        }

        public Task<IList<T>> GetAllAsync<TKey>(Expression<Func<T, bool>> filter, Func<T, TKey> orderBy, int skip, int take) {
            var entities = Session.Query<T>().Where(filter).OrderBy(orderBy).Skip(skip).Take(take).ToList();
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

        private readonly NHibernateUnitOfWork _nHibernateUnitOfWork;
    }

    public interface IRepository<T> {
        Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> filter, int skip, int take);

        Task<IList<T>> GetAllAsync<TKey>(Expression<Func<T, bool>> filter, Func<T, TKey> orderBy, int skip, int take);

        Task<IList<T>> GetAllAsync<TKey>(Func<T, TKey> orderBy, bool asc);

        Task<T> GetAsync(object id);

        Task<int> CountAsync();

        Task<int> CountAsync(Expression<Func<T, bool>> filter);

        Task UpdateAsync(T entity);
    }
}
