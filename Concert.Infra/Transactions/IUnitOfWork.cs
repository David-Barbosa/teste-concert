using System;

namespace Concert.Infra.Transactions
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }
}
