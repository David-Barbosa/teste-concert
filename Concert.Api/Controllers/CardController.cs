using System.Threading.Tasks;
using Concert.Domain.Commands.Handlers;
using Concert.Domain.Commands.Inputs.CardCommands;
using Concert.Domain.Interfaces;
using Concert.Infra.Transactions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Concert.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CardController : BaseController
    {
        private readonly CardCommandHandler _handler;
        private readonly ICardRepository _cardRepository;

        public CardController(IUnitOfWork unitOfWork, CardCommandHandler handler, ICardRepository cardRepository) : base(unitOfWork)
        {
            _handler = handler;
            _cardRepository = cardRepository;
        }


        [HttpPost]
        [AllowAnonymous]
        [Route("card")]
        public async Task<IActionResult> Post([FromBody] RegisterCardCommand command)
        {
            var result = _handler.Handle(command);
            return await Response(result, _handler.Notifications);
        }
    }
}