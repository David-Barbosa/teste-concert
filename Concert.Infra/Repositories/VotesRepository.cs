using Concert.Domain.Entities;
using Concert.Domain.Interfaces;
using Concert.Infra.Context;

namespace Concert.Infra.Repositories
{
    public class VotesRepository : Repository<Votes>, IVotesRepository
    {
        private readonly ConcertDataContext _context;

        public VotesRepository(ConcertDataContext context) : base(context) => _context = context;
    }
}
