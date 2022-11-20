#nullable enable
using System.Collections.Generic;

namespace Dinkle.Entities
{
    public class EntityInfo
    {
        public EntityInfo(string id, string name, string createdTime, string editedTime, IEnumerable<string> tags, string type, int size, string status, string statusReason, string errorMessage, string? templateId, string? detail)
        {
            Id = id;
            Name = name;
            CreatedTime = createdTime;
            EditedTime = editedTime;
            Tags = tags;
            Type = type;
            Size = size;
            Status = status;
            StatusReason = statusReason;
            ErrorMessage = errorMessage;
            TemplateId = templateId;
            Detail = detail;
        }

        public string Id { get; }
        public string Name { get; }
        public string CreatedTime { get; }
        public string EditedTime { get; }
        public IEnumerable<string> Tags { get; }
        public string Type { get; }
        public int Size { get; }
        public string Status { get; }
        public string StatusReason { get; }
        public string ErrorMessage { get; }
        public string? TemplateId { get; } 
        public string? Detail { get; } 
    }
}