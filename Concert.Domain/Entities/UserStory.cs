using concert.Domain.Entities;
using FluentValidator.Validation;

namespace Concert.Domain.Entities
{
    public class UserStory : Entity
    {
        public UserStory(){}

        public UserStory(string description)
        {
            Description = description;

            AddNotifications(new ValidationContract().IsNotNullOrEmpty(Description, "Descrição", "Descrição não pode ser nulo"));
            AddNotifications(new ValidationContract().HasMinLen(Description, 20, "Descriçao", "Descrição deve conter no mínimo 20 caracteres"));
        }

        public string Description { get; private set; }
    }
}
