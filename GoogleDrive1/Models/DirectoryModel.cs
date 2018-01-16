using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Microsoft.Extensions.FileSystemGlobbing.Internal.PathSegments;

namespace GoogleDrive1.Models
{
    public class DirectoryModel
    {
        public string Id { get; set; }
        public IList<Google.Apis.Drive.v3.Data.File> Files { get; set; }
        public Stack<string> Parents = new Stack<string>();

        public static DirectoryModel Instance { get; } = new DirectoryModel();

        private DirectoryModel()
        {
        }

        public void GetDirectory(File file)
        {
            var filename = file.Id ?? "root";

            if (file.MimeType != "application/vnd.google-apps.folder" && file.Id != null) return;
            if (Parents.Contains(file.Name))
                while (Parents.Count > 0 && Parents.Peek() != file.Name)
                    Parents.Pop();
            else
                Parents.Push(file.Name);

            // Define parameters of request.
            FilesResource.ListRequest listRequest = DataAccess.DriveService.Files.List();
            listRequest.Q = $"\'{filename}\' in parents";
            listRequest.OrderBy = "folder, name asc";
            listRequest.PageSize = 1000;
            listRequest.Fields = "nextPageToken, files(id, name)";
            // List files.
            Files = listRequest.Execute().Files;
        }
    }
}
