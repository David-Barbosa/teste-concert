using Concert.Shared.Commands;

namespace Concert.Domain.Commands.Inputs.UserCommands
{
    public class RegisterUserCommand : ICommand
    {
        public string Name { get; set; }
    }
}
