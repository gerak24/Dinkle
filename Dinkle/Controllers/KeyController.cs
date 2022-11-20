using System.Threading;
using System.Threading.Tasks;
using Dinkle.Application.ApiKey.Commanda;
using Dinkle.Core.Buses;
using Microsoft.AspNetCore.Mvc;

namespace Dinkle.Controllers
{
    public class KeyController : ApiController
    {
        private readonly IQueryBus _queries;
        private readonly ICommandBus _commands;

        public KeyController(ICommandBus commands, IQueryBus queries)
        {
            _commands = commands;
            _queries = queries;
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(AddApiKeyCommand cmd, CancellationToken ct = default)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var file = await _commands.Send(cmd, ct);
            if (file == null) return BadRequest();
            return Accepted(file);
        }
    }
}