using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dinkle.Application.Folders.Commands;
using Dinkle.Core.Entities;
using Dinkle.Core.Handlers;
using Dinkle.Entities;

namespace Dinkle.Application.Folders.Handlers
{
    public class DownloadFileCommandHandlerd : ICommandHandler<DownloadFileCommand, string>
    {
        private HttpClient _client = new();
        private readonly IServerEntities _entities;

        public DownloadFileCommandHandlerd(IServerEntities entities)
        {
            _entities = entities;
        }


        public async Task<string> Handle(DownloadFileCommand request, CancellationToken cancellationToken)
        {
            var auth = Convert.ToBase64String(Encoding.UTF8.GetBytes($"apikey:{request.ApiKey}"));
            _client.DefaultRequestHeaders.Add("Authorization", $"Basic {auth}");
            
            HttpResponseMessage response;
            switch (request.Type)
            {
                case EntityType.Template:
                    response = await _client.GetAsync($"https://fastreport.cloud/download/t/{request.Id}",
                        cancellationToken);
                    var bb = await response.Content.ReadAsStringAsync(cancellationToken);
                    break;
                case EntityType.Report:
                    response = await _client.GetAsync($"https://fastreport.cloud/download/r/{request.Id}",
                        cancellationToken);
                    break;
                case EntityType.Export:
                    response = await _client.GetAsync($"https://fastreport.cloud/download/es/{request.Id}",
                        cancellationToken);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            
            return await response.Content.ReadAsStringAsync(cancellationToken);
        }
    }
}