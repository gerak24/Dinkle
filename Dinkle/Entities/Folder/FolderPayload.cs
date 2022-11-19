#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dinkle.Entities.Folder
{
    public class FolderPayload
    {
        public FolderPayload(IEnumerable<Folder>? folders, IEnumerable<File>? files)
        {
            Folders = folders ?? Enumerable.Empty<Folder>();
            Files = files ?? ArraySegment<File>.Empty;
        }

        public IEnumerable<Folder>? Folders { get; }
        public IEnumerable<File>? Files { get; }
    }
}