using System;
using Dinkle.Core.Queries;
using Dinkle.Entities.Account;

namespace Dinkle.Application.Profiles.Queries
{
    public class GetProfileQuery : IQuery<Account>
    {
        public GetProfileQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}