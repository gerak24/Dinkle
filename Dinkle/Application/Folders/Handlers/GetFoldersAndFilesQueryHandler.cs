using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dinkle.Application.Folders.Queries;
using Dinkle.Core.Entities;
using Dinkle.Core.Handlers;
using Dinkle.Entities;
using Dinkle.Entities.Folder;
using Newtonsoft.Json;

namespace Dinkle.Application.Folders.Handlers
{
    public class GetFoldersAndFilesQueryHandler : IQueryHandler<GetFoldersAndFilesQuery, FolderPayload>
    {
        private HttpClient _client = new();
        private readonly IServerEntities _entities;

        public GetFoldersAndFilesQueryHandler(IServerEntities entities)
        {
            _entities = entities;
        }

        public async Task<FolderPayload> Handle(GetFoldersAndFilesQuery request, CancellationToken cancellationToken)
        {
            var auth = Convert.ToBase64String(Encoding.UTF8.GetBytes($"apikey:{request.ApiKey}"));
            _client.DefaultRequestHeaders.Add("Authorization", $"Basic {auth}");
            
            HttpResponseMessage response;
            switch (request.Type)
            {
                case EntityType.Export:
                    response = await _client.GetAsync($"https://fastreport.cloud/api/rp/v1/Exports/Folder/{request.Id}/ListFolderAndFiles",
                        cancellationToken);
                    break;
                case EntityType.Template:
                    response = await _client.GetAsync($"https://fastreport.cloud/api/rp/v1/Templates/Folder/{request.Id}/ListFolderAndFiles",
                        cancellationToken);
                    break;
                case EntityType.Report:
                    response = await _client.GetAsync($"https://fastreport.cloud/api/rp/v1/Reports/Folder/{request.Id}/ListFolderAndFiles",
                        cancellationToken);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            var status = JsonConvert.DeserializeObject<FolderPayload>(await response.Content.ReadAsStringAsync(cancellationToken));
            return status;
        }
    }
}