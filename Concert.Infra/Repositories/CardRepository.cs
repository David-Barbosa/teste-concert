using Concert.Domain.Entities;
using Concert.Domain.Interfaces;
using Concert.Infra.Context;

namespace Concert.Infra.Repositories
{
    public class CardRepository : Repository<Card>, ICardRepository
    {
        private readonly ConcertDataContext _context;

        public CardRepository(ConcertDataContext context) : base(context) => _context = context;
    }
}