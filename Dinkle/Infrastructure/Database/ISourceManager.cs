using System;
using Dinkle.Core.Entities;

namespace Dinkle.Infrastructure.Database
{
    public interface ISourceManager
    {
        IUserEntities GetUserEntities(Guid tenantId);
        IServerEntities GetServerEntities();
    }
}