using System;
using Dinkle.Core.Entities;

namespace Dinkle.Entities
{
     public abstract class Entity : IEntity
    {
        public Guid Id { get; protected set; }
    }
}