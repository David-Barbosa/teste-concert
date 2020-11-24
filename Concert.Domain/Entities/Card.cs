using concert.Domain.Entities;
using FluentValidator.Validation;

namespace Concert.Domain.Entities
{
    public class Card : Entity
    {
        public Card(){}

        public Card(int valueCard)
        {
            ValueCard = valueCard;

            AddNotifications(new ValidationContract().IsGreaterThan(ValueCard, 0, "ValueCard", "Valor deve ser maior que 0"));
        }

        public int ValueCard { get; private set; }
    }
}
