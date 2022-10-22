using System.Threading;
using System.Threading.Tasks;
using Dinkle.Application.Profiles.Queries;
using Dinkle.Core.Handlers;
using Dinkle.Entities.Account;

namespace Dinkle.Application.Profiles.Handlers
{
    public class GetProfileQueryHandler : IQueryHandler<GetProfileQuery, Account>
    {
        
        
        public Task<Account> Handle(GetProfileQuery request, CancellationToken cancellationToken)
        {
            return null;
        }
    }
}