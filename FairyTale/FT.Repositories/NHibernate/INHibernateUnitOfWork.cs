using NHibernate;

namespace FT.Repositories.NHibernate {
    public interface INHibernateUnitOfWork : IUnitOfWork {
        ISession Session { get; set; }
    }
}