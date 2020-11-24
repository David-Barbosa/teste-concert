using concert.Domain.Entities;

namespace Concert.Domain.Entities
{
    public class Votes : Entity
    {

        public Votes(int userId, int cardId, int userStoryId)
        {
            UserId = userId;
            CardId = cardId;
            UserStoryId = userStoryId;
        }

        public int UserId { get; private set; }
        public virtual User User { get; private set; }

        public int CardId { get; private set; }
        public virtual Card Card { get; private set; }

        public int UserStoryId { get; private set; }
        public virtual UserStory UserStory { get; private set; }

        public override string ToString() => $"Usuário {UserId} - votou {Card.ValueCard} para a história {UserStory.Description}";
    }
}
