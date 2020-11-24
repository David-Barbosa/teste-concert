using Concert.Domain.Commands.Inputs.VotesCommands;
using Concert.Domain.Commands.Outputs;
using Concert.Domain.Entities;
using Concert.Domain.Interfaces;
using Concert.Domain.RabbitMQHelper;
using Concert.Shared.Commands;
using FluentValidator;
using RabbitMQ.Client;

namespace Concert.Domain.Commands.Handlers
{
    public class VotesCommandHandler : Notifiable, ICommandHandler<RegisterVotesCommand>
    {
        private readonly IVotesRepository _votesRepository;
        private readonly IRabbitMQHelper _rabbitMQHelper;
        IConnection _connection = null;
        private const string queueName = "AllVotes";

        public VotesCommandHandler(IVotesRepository votesRepository, IRabbitMQHelper rabbitMQHelper)
        {
            _votesRepository = votesRepository;
            _rabbitMQHelper = rabbitMQHelper;
        }

        public ICommandResult Handle(RegisterVotesCommand command)
        {
            var votes = new Votes(command.UserId, command.CardId, command.UserStoryId);

            AddNotifications(votes.Notifications);

            if (Invalid)
                return new CommandResult(false, "Por favor, corrija os campos abaixo", Notifications);

            var connectionFactory = _rabbitMQHelper.GetConnectionFactory();

            using (_connection = _rabbitMQHelper.CreateConnection(connectionFactory))
            {
                _rabbitMQHelper.WriteMessageOnQueue(votes.ToString(), queueName, _connection);
            }

            _votesRepository.Add(votes);

            return new CommandResult(true, "Cadastro realizado com sucesso", votes);
        }
    }
}
