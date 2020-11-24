using concert.Domain.Entities;

namespace Concert.Domain.Entities
{
    public class Card : Entity
    {
        public Card(){}

        public Card(int valueCard)
        {
            ValueCard = valueCard;
        }

        public int ValueCard { get; private set; }
    }
}
