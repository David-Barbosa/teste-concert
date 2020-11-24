using System.Threading.Tasks;
using Concert.Domain.Commands.Handlers;
using Concert.Domain.Commands.Inputs.UserCommands;
using Concert.Domain.Interfaces;
using Concert.Infra.Transactions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Concert.Api.Controllers
{
    [Route("[controller]")]
    public class UserController : BaseController
    {
        private readonly UserCommandHandler _handler;
        private readonly IUserRepository _userRepository;

        public UserController(IUnitOfWork unitOfWork, UserCommandHandler handler, IUserRepository userRepository) : base(unitOfWork)
        {
            _handler = handler;
            _userRepository = userRepository;
        }


        [HttpPost]
        [AllowAnonymous]
        [Route("user")]
        public async Task<IActionResult> Post([FromBody] RegisterUserCommand command)
        {
            var result = _handler.Handle(command);
            return await Response(result, _handler.Notifications);
        }
    }
}