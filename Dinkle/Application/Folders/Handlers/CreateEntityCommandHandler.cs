using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dinkle.Application.Folders.Commands;
using Dinkle.Core.Entities;
using Dinkle.Core.Handlers;
using MediatR;
using Newtonsoft.Json;

namespace Dinkle.Application.Folders.Handlers
{
    public class CreateEntityCommandHandler : ICommandHandler<CreateEntityCommand>
    {
        private HttpClient _client = new();
        private IServerEntities _entities;

        public CreateEntityCommandHandler(IServerEntities entities)
        {
            _entities = entities;
        }

        public async Task<Unit> Handle(CreateEntityCommand request, CancellationToken cancellationToken)
        {
            var auth = Convert.ToBase64String(Encoding.UTF8.GetBytes($"apikey:{request.ApiKey}"));
            _client.DefaultRequestHeaders.Add("Authorization", $"Basic {auth}");

            HttpResponseMessage response;
            object content;
            content = null;
            if (request.IsFolder)
            {
                content = new
                {
                    request.Name,
                    request.Tags
                };
                response = await _client.PostAsJsonAsync(
                    $"https://fastreport.cloud/api/rp/v1/{Enum.GetName(request.Type)}s/Folder/{request.FolderId}/Folder",
                    content,
                    cancellationToken);
            }
            else
            {
                content = new
                {
                    request.Name,
                    request.Tags,
                    request.Content
                };
                response = await _client.PostAsJsonAsync(
                    $"https://fastreport.cloud/api/rp/v1/{Enum.GetName(request.Type)}s/Folder/{request.FolderId}/File",
                    content,
                    cancellationToken);
            }

            var newItem =
                JsonConvert.DeserializeObject<Entities.File>(
                    await response.Content.ReadAsStringAsync(cancellationToken));
            if (newItem != null)
            {
                response = await _client.PutAsJsonAsync(
                    $"https://fastreport.cloud/api/rp/v1/{Enum.GetName(request.Type)}s/{newItem.Type}/{newItem.Id}/permissions",
                    new
                    {
                        newPermissions = new
                        {
                            owner = new
                            {
                                create = 1,
                                delete = 1,
                                execute = 1,
                                get = 1,
                                update = 1,
                                administrate = 8
                            }
                        }
                    },
                    cancellationToken);
            }

            return Unit.Value;
        }
    }
}