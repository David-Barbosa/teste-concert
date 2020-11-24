using Concert.Domain.Entities;
using Concert.Domain.Interfaces;
using Concert.Infra.Context;

namespace Concert.Infra.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly ConcertDataContext _context;

        public UserRepository(ConcertDataContext context) : base(context) => _context = context;
    }
}
