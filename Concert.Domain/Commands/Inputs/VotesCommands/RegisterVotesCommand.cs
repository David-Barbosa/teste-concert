using Concert.Shared.Commands;

namespace Concert.Domain.Commands.Inputs.VotesCommands
{
    public class RegisterVotesCommand : ICommand
    {
        public int UserId { get; set; }

        public int CardId { get; set; }

        public int UserStoryId { get; set; }
    }
}
