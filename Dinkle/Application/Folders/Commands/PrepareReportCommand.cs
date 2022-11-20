#nullable enable
using System.Collections.Generic;
using Dinkle.Core.Commands;

namespace Dinkle.Application.Folders.Commands
{
    public class PrepareReportCommand : ICommand
    {
        public PrepareReportCommand(string parentFolderId, string apiKey, string name, IEnumerable<string>? reportParameters, string templateId)
        {
            ParentFolderId = parentFolderId;
            ApiKey = apiKey;
            Name = name;
            ReportParameters = reportParameters;
            TemplateId = templateId;
        }

        public string TemplateId { get; }
        public string ParentFolderId { get; }
        public string ApiKey { get; }
        public string Name { get; }
        public IEnumerable<string>? ReportParameters { get; }
    }
}