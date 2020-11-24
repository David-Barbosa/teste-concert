using Concert.Domain.Commands.Inputs.CardCommands;
using Concert.Domain.Commands.Outputs;
using Concert.Domain.Entities;
using Concert.Domain.Interfaces;
using Concert.Shared.Commands;
using FluentValidator;

namespace Concert.Domain.Commands.Handlers
{
    public class CardCommandHandler : Notifiable, ICommandHandler<RegisterCardCommand>
    {
        private readonly ICardRepository _cardRepository;

        public CardCommandHandler(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }
        public ICommandResult Handle(RegisterCardCommand command)
        {
            var card = new Card(command.ValueCard);

            AddNotifications(card.Notifications);

            if (Invalid)
                return new CommandResult(false, "Por favor, corrija os campos abaixo", Notifications);

            _cardRepository.Add(card);

            return new CommandResult(true, "Cadastro realizado com sucesso", card);
        }
    }
}
