using System.Threading;
using System.Threading.Tasks;
using Dinkle.Application.Accounts.Commands;
using Dinkle.Application.Accounts.Queries;
using Dinkle.Core.Buses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dinkle.Controllers
{
    public class AccountController : ApiController
    {
        private readonly IQueryBus _queries;
        private readonly ICommandBus _commands;

        public AccountController(IQueryBus queries, ICommandBus commands)
        {
            _queries = queries;
            _commands = commands;
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<IActionResult> Register(RegisterAccountCommand cmd, CancellationToken ct = default)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _commands.Send(cmd, ct);
            if (result.IsSuccsessful)
                return Accepted();

            return BadRequest(result.Messages);
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<IActionResult> Login(AuthorizeAccountQuery cmd, CancellationToken ct = default)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _queries.Send(cmd, ct);
            if (result.IsSuccsessful)
                return Json(new
                {
                    result.Token,
                    result.ApiKey
                });

            return BadRequest(result.Messages);
        }
        
        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<IActionResult> RecoverPassword(RecoverPasswordCommand cmd, CancellationToken ct = default)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _commands.Send(cmd, ct);
            if (result.IsSuccsessful)
                return Accepted();

            return BadRequest(result.Messages);
        }
    }
}