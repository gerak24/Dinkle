using System;

namespace Dinkle.Core.Entities
{
    public interface IEntity
    {
        /// <summary>
        /// Id of entity
        /// </summary>
        public Guid Id { get; }
    }
}