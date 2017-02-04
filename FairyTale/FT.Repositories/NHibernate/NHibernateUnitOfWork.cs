using System;
using NHibernate;

namespace FT.Repositories.NHibernate {
    public class NHibernateUnitOfWork : INHibernateUnitOfWork {
        public ISession Session { get; set; }

        public NHibernateUnitOfWork(ISessionFactory sessionFactory) {
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
}
