using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;

namespace FT.Repositories {
    public class UnitOfWork : IUnitOfWork {
        public ISession Session { get; set; }

        public UnitOfWork(ISessionFactory sessionFactory) {
            Session = sessionFactory.OpenSession();
        }

        public void BeginTransaction() {
            _transaction = Session.BeginTransaction();
        }

        public void Commit() {
            try {
                if (_transaction != null && _transaction.IsActive)
                    _transaction.Commit();
            }
            catch {
                if (_transaction != null && _transaction.IsActive)
                    _transaction.Rollback();

                throw;
            }
            finally {
                Session.Dispose();
            }
        }

        public void Rollback() {
            try {
                if (_transaction != null && _transaction.IsActive)
                    _transaction.Rollback();
            }
            finally {
                Session.Dispose();
            }
        }

        public void Dispose() {
            Session.Dispose();
        }

        private ITransaction _transaction;
    }

    public interface IUnitOfWork : IDisposable {
        void BeginTransaction();
        void Commit();
        void Rollback();
    }
}
