using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Concert.Infra.Transactions;
using FluentValidator;
using Microsoft.AspNetCore.Mvc;

namespace Concert.Api.Controllers
{
    public class BaseController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public BaseController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        protected async Task<IActionResult> Response(object result, IEnumerable<Notification> notifications)
        {
            if (notifications.Any())
            {
                return Ok(new
                {
                    success = false,
                    errors = notifications
                });
            }

            try
            {
                _unitOfWork.Commit();
                return Ok(new
                {
                    success = true,
                    data = result
                });
            }
            catch
            {
                return BadRequest(new
                {
                    success = false,
                    errors = new[] { "Ocorreu uma falha interna no servidor." }
                });
            }
        }
    }
}