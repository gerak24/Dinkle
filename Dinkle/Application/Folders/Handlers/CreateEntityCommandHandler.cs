using System;
using System.Buffers.Text;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Unicode;
using System.Threading;
using System.Threading.Tasks;
using Dinkle.Application.Folders.Commands;
using Dinkle.Application.Folders.Queries;
using Dinkle.Core.Buses;
using Dinkle.Core.Entities;
using Dinkle.Core.Handlers;
using Dinkle.Entities;
using Newtonsoft.Json;

namespace Dinkle.Application.Folders.Handlers
{
    public class CreateEntityCommandHandler : ICommandHandler<CreateEntityCommand, EntityInfo>
    {
        private HttpClient _client = new();
        private IServerEntities _entities;
        private IQueryBus _queries;

        public CreateEntityCommandHandler(IServerEntities entities, IQueryBus queries)
        {
            _entities = entities;
            _queries = queries;
        }

        public async Task<EntityInfo> Handle(CreateEntityCommand request, CancellationToken cancellationToken)
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

            var bb = await response.Content.ReadAsStringAsync(cancellationToken);
            var newItem =
                JsonConvert.DeserializeObject<File>( 
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
                return await _queries.Send(new GetEntityInfoQuery(newItem.Id, request.ApiKey, request.Type),
                    cancellationToken);
            }

            return new EntityInfo(string.Empty, request.Name, string.Empty, string.Empty,
                request.Tags ?? ArraySegment<string>.Empty, Enum.GetName(request.Type) ?? string.Empty, 0, "400", string.Empty,
                "Error when create", null, "Error when create");
        }
    }
}