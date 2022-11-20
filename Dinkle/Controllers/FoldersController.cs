using System.Threading;
using System.Threading.Tasks;
using Dinkle.Application.Folders.Commands;
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
        [HttpPost("[action]")]
        public async Task<IActionResult> DownloadFile(DownloadFileCommand cmd, CancellationToken ct = default)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var file = await _commands.Send(cmd, ct);
            if (file == null) return BadRequest();
            return Accepted(file);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateEntityCommand cmd, CancellationToken ct = default)    
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Accepted(await _commands.Send(cmd, ct));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Prepare(PrepareReportCommand cmd, CancellationToken ct = default)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _commands.Publish(cmd, ct);
            
            return Accepted();
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> GetFileInfo(GetEntityInfoQuery cmd, CancellationToken ct = default)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);  
            
            return Accepted( await _queries.Send(cmd, ct));
        }
    }
}