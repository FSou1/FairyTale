using System;

namespace FT.Repositories {
    public interface IUnitOfWork : IDisposable {
        void BeginTransaction();
        void Commit();
        void Rollback();
    }
}