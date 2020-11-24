using Concert.Shared.Commands;

namespace Concert.Domain.Commands.Inputs.UserStoryCommands
{
    public class RegisterUserStoryCommand : ICommand
    {
        public string Description { get; set; }
    }
}
