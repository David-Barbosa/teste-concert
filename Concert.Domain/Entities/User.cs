using concert.Domain.Entities;
using FluentValidator.Validation;

namespace Concert.Domain.Entities
{
    public class User : Entity
    {
        public User() {}

        public User(string name)
        {
            Name = name;

            AddNotifications(new ValidationContract().IsNotNullOrEmpty(Name, "Nome", "Nome não pode ser nulo"));
            AddNotifications(new ValidationContract().HasMinLen(Name, 3, "Nome", "Nome deve conter no mínimo 3 caracteres"));
        }

        public string Name { get; private set; }

    }
}
