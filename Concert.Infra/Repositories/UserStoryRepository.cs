using Concert.Domain.Entities;
using Concert.Domain.Interfaces;
using Concert.Infra.Context;

namespace Concert.Infra.Repositories
{
    public class UserStoryRepository : Repository<UserStory>, IUserStoryRepository
    {
        private readonly ConcertDataContext _context;

        public UserStoryRepository(ConcertDataContext context) : base(context) => _context = context;
    }
}
