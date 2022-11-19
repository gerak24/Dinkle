#nullable enable
using System;
using System.Collections.Generic;

namespace Dinkle.Entities.Folder
{
    public class FolderPayload
    {
        public FolderPayload(IEnumerable<File>? files)
        {
            Files = files ?? ArraySegment<File>.Empty;
        }
        public IEnumerable<File>? Files { get; }
    }
}