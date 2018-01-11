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
            Parents.Push("root");
        }

        public void GetDirectory(string folder)
        {
            Parents.Push(folder);

            // Define parameters of request.
            FilesResource.ListRequest listRequest = DataAccess.DriveService.Files.List();
            listRequest.Q = $"mimeType=\'application/vnd.google-apps.folder\' and \'{folder}\' in parents";
            listRequest.OrderBy = "name asc";
            listRequest.PageSize = 1000;
            listRequest.Fields = "nextPageToken, files(id, name)";
            // List files.
            Files = listRequest.Execute().Files;
        }
    }
}
