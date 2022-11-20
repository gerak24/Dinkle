using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dinkle.Application.Folders.Queries;
using Dinkle.Core.Entities;
using Dinkle.Core.Handlers;
using Dinkle.Entities;
using Newtonsoft.Json;

namespace Dinkle.Application.Folders.Handlers
{
    public class GetEntityInfoQueryрHandler : IQueryHandler<GetEntityInfoQuery, EntityInfo>
    {
        private HttpClient _client = new();
        private readonly IServerEntities _entities;

        public GetEntityInfoQueryрHandler(IServerEntities entities)
        {
            _entities = entities;
        }

        public async Task<EntityInfo> Handle(GetEntityInfoQuery request, CancellationToken cancellationToken)
        {
            var auth = Convert.ToBase64String(Encoding.UTF8.GetBytes($"apikey:{request.ApiKey}"));
            _client.DefaultRequestHeaders.Add("Authorization", $"Basic {auth}");

            var response = await _client.GetAsync($"https://fastreport.cloud/api/rp/v1/{request.Type}s/File/{request.Id}",
                cancellationToken);
            var status = JsonConvert.DeserializeObject<EntityInfo>(await response.Content.ReadAsStringAsync(cancellationToken));
            return status;
        }
    }
}