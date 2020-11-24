using Concert.Domain.Commands.Inputs.UserCommands;
using Concert.Domain.Commands.Outputs;
using Concert.Domain.Entities;
using Concert.Domain.Interfaces;
using Concert.Shared.Commands;
using FluentValidator;

namespace Concert.Domain.Commands.Handlers
{
    public class UserCommandHandler : Notifiable, ICommandHandler<RegisterUserCommand>
    {
        private readonly IUserRepository _userRepository;

        public UserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public ICommandResult Handle(RegisterUserCommand command)
        {
            var user = new User(command.Name);

            AddNotifications(user.Notifications);

            if (Invalid)
                return new CommandResult(false, "Por favor, corrija os campos abaixo", Notifications);

            _userRepository.Add(user);

            return new CommandResult(true, "Cadastro realizado com sucesso", user);
        }
    }
}
