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
    public class GetRootFolderQueryHandler : IQueryHandler<GetRootFolderQuery, Folder>
    {
        private HttpClient _client = new();
        private readonly IServerEntities _entities;

        public GetRootFolderQueryHandler(IServerEntities entities)
        {
            _entities = entities;
        }

        public async Task<Folder> Handle(GetRootFolderQuery request, CancellationToken cancellationToken)
        {
            var auth = Convert.ToBase64String(Encoding.UTF8.GetBytes($"apikey:{request.ApiKey}"));
            _client.DefaultRequestHeaders.Add("Authorization", $"Basic {auth}");
            HttpResponseMessage response;
            switch (request.Type)
            {
                case EntityType.Export:
                    response = await _client.GetAsync("https://fastreport.cloud/api/rp/v1/Exports/Root",
                        cancellationToken);
                    break;
                case EntityType.Template:
                    response = await _client.GetAsync("https://fastreport.cloud/api/rp/v1/Templates/Root",
                        cancellationToken);
                    break;
                case EntityType.Report:
                    response = await _client.GetAsync("https://fastreport.cloud/api/rp/v1/Reports/Root",
                        cancellationToken);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            var status =
                JsonConvert.DeserializeObject<Folder>(await response.Content.ReadAsStringAsync(cancellationToken));
            return status;
        }
    }
}