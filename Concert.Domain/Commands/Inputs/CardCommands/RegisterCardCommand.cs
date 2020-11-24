using Concert.Shared.Commands;

namespace Concert.Domain.Commands.Inputs.CardCommands
{
    public class RegisterCardCommand : ICommand
    {
        public int ValueCard { get; set; }
    }
}
