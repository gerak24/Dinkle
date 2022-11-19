using System.Threading;
using System.Threading.Tasks;
using Dinkle.Application.Folders.Queries;
using Dinkle.Core.Buses;
using Microsoft.AspNetCore.Mvc;

namespace Dinkle.Controllers
{
    public class FolderController : ApiController
    {
        private readonly IQueryBus _queries;
        private readonly ICommandBus _commands;

        public FolderController(ICommandBus commands, IQueryBus queries)
        {
            _commands = commands;
            _queries = queries;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> GetRootFolder(GetRootFolderQuery cmd, CancellationToken ct = default)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Accepted(await _queries.Send(cmd, ct));
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> GetFoldersAndFiles(GetFoldersAndFilesQuery cmd, CancellationToken ct = default)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            return Accepted(await _queries.Send(cmd, ct));
        }
    }
}