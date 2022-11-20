using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dinkle.Application.ApiKey.Commanda;
using Dinkle.Core.Entities;
using Dinkle.Core.Handlers;
using Dinkle.Entities.Account.Data;
using Newtonsoft.Json;

namespace Dinkle.Application.ApiKey.Handlers
{
    public class AddApiKeyCommandHandler : ICommandHandler<AddApiKeyCommand, Entities.Account.Data.ApiKey>
    {
        private HttpClient _client = new();
        private readonly IServerEntities _entities;

        public AddApiKeyCommandHandler(IServerEntities entities)
        {
            _entities = entities;
        }

        public async Task<Entities.Account.Data.ApiKey> Handle(AddApiKeyCommand request,
            CancellationToken cancellationToken)
        {
            var auth = Convert.ToBase64String(Encoding.UTF8.GetBytes($"apikey:{request.ApiKey}"));
            _client.DefaultRequestHeaders.Add("Authorization", $"Basic {auth}");

            var response = await _client.PostAsJsonAsync($"https://fastreport.cloud/api/manage/v1/ApiKeys", new
                {
                    request.Description,
                    expired = "2023-11-20T10:32:24.971Z"
                },
                cancellationToken);
            var bb = await response.Content.ReadAsStringAsync(cancellationToken);
            var result = JsonConvert.DeserializeObject<ApiValue>(
                await response.Content.ReadAsStringAsync(cancellationToken));
            if (response.StatusCode < HttpStatusCode.BadRequest && result != null)
            {
                return new Entities.Account.Data.ApiKey(result.Description,result.Value);
            }

            return null;
        }
    }
}