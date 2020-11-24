using Concert.Infra.Context;

namespace Concert.Infra.Transactions
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ConcertDataContext _context;

        public UnitOfWork(ConcertDataContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
