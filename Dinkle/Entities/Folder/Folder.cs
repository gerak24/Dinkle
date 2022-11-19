using System;
using Dinkle.Core.Entities;

namespace Dinkle.Entities.Folder
{
    public class Folder
    {
        public Folder(string name, string subscriptionId, string id)
        {
            Name = name;
            SubscriptionId = subscriptionId;
            Id = id;
        }
        public string Name { get; }
        public string SubscriptionId { get; }
        public string Id { get; }
    }
}
/*
"name": "RootFolder",
  "type": "Folder",
  "size": 16384,
  "subscriptionId": "6377865f5f620ebfce9a07ce",
  "status": "None",
  "statusReason": "None",
  "id": "6377865f5f620ebfce9a07cb",
  "createdTime": "2022-11-18T13:19:27.115Z",
  "creatorUserId": "2df79f83-07f1-41ba-96b5-7757bbf377df",
  "editedTime": "2022-11-18T17:24:33.086Z",
  "editorUserId": "bdbb27bf-10f8-4b96-aa58-c69d68415b8e"
  */