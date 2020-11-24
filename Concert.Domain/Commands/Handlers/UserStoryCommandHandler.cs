using Concert.Domain.Commands.Inputs.UserStoryCommands;
using Concert.Domain.Commands.Outputs;
using Concert.Domain.Entities;
using Concert.Domain.Interfaces;
using Concert.Shared.Commands;
using FluentValidator;

namespace Concert.Domain.Commands.Handlers
{
    public class UserStoryCommandHandler : Notifiable, ICommandHandler<RegisterUserStoryCommand>
    {
        private readonly IUserStoryRepository _userStoryRepository;

        public UserStoryCommandHandler(IUserStoryRepository userStoryRepository)
        {
            _userStoryRepository = userStoryRepository;
        }

        public ICommandResult Handle(RegisterUserStoryCommand command)
        {
            var userStory = new UserStory(command.Description);

            AddNotifications(userStory.Notifications);

            if (Invalid)
                return new CommandResult(false, "Por favor, corrija os campos abaixo", Notifications);

            _userStoryRepository.Add(userStory);

            return new CommandResult(true, "Cadastro realizado com sucesso", userStory);
        }
    }
}
