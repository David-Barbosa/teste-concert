using Concert.Api.ConfigHub;
using Concert.Domain.Commands.Handlers;
using Concert.Domain.Commands.Inputs.VotesCommands;
using Concert.Domain.Interfaces;
using Concert.Infra.Transactions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Concert.Api.Controllers
{
    [Route("[controller]")]
    public class VotesController : BaseController
    {
        private readonly VotesCommandHandler _handler;
        private readonly IVotesRepository _votesRepository;
        private readonly IHubContext<VotesHub> _hubVotes;

        public VotesController(IUnitOfWork unitOfWork,
                               VotesCommandHandler handler,
                               IVotesRepository votesRepository,
                               IHubContext<VotesHub> hubVotes) : base(unitOfWork)
        {
            _handler = handler;
            _votesRepository = votesRepository;
            _hubVotes = hubVotes;
        }


        [HttpPost]
        [AllowAnonymous]
        [Route("votes")]
        public async Task<IActionResult> Post([FromBody] RegisterVotesCommand command)
        {
            var result = _handler.Handle(command);
            await WriteOnStream(result.ToString());

            return await Response(result, _handler.Notifications);
        }

        private async Task WriteOnStream(string action)
        {
            //Utiliza o Hub para enviar uma mensagem para ReceiveMessage
            await _hubVotes.Clients.All.SendAsync("ReceiveMessage", action);
        }
    }
}
