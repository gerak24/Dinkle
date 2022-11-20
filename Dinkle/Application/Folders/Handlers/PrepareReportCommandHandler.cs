using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dinkle.Application.Folders.Commands;
using Dinkle.Core.Entities;
using Dinkle.Core.Handlers;
using MediatR;

namespace Dinkle.Application.Folders.Handlers
{
    public class PrepareReportCommandHandler : ICommandHandler<PrepareReportCommand>
    {
        private HttpClient _client = new();
        private IServerEntities _entities;
        private ConditionalWeakTable<string, object> _depthValues = new();

        public PrepareReportCommandHandler(IServerEntities entities)
        {
            _entities = entities;
        }


        public async Task<Unit> Handle(PrepareReportCommand request, CancellationToken cancellationToken)
        {
            var auth = Convert.ToBase64String(Encoding.UTF8.GetBytes($"apikey:{request.ApiKey}"));
            _client.DefaultRequestHeaders.Add("Authorization", $"Basic {auth}");

            object content = new
            {
                request.Name,
                request.ParentFolderId,
            };
            
            var response =
                await _client.PostAsJsonAsync(
                    $"https://fastreport.cloud/api/rp/v1/Templates/File/{request.TemplateId}/Prepare", content,
                    cancellationToken: cancellationToken);
            var bb = await response.Content.ReadAsStringAsync(cancellationToken);
            return Unit.Value;
        }
    }
}