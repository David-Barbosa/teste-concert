using Concert.Domain.Commands.Handlers;
using Concert.Domain.Commands.Inputs.UserStoryCommands;
using Concert.Domain.Interfaces;
using Concert.Infra.Transactions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Concert.Api.Controllers
{
    [Route("[controller]")]
    public class UserStoryController : BaseController
    {
        private readonly UserStoryCommandHandler _handler;
        private readonly IUserStoryRepository _userStoryRepository;

        public UserStoryController(IUnitOfWork unitOfWork, UserStoryCommandHandler handler, IUserStoryRepository userStoryRepository) : base(unitOfWork)
        {
            _handler = handler;
            _userStoryRepository = userStoryRepository;
        }


        [HttpPost]
        [AllowAnonymous]
        [Route("user_story")]
        public async Task<IActionResult> Post([FromBody] RegisterUserStoryCommand command)
        {
            var result = _handler.Handle(command);
            return await Response(result, _handler.Notifications);
        }
    }
}
