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
        public IList<File> Files { get; set; }
        public Stack<File> Parents = new Stack<File>();

        public static DirectoryModel Instance { get; } = new DirectoryModel();

        private DirectoryModel()
        {            
        }

        public void GetDirectory(string fileId)
        {
            File file = Parents.First(f => f.Id == fileId);
            if (file != null)
                while (Parents.Count > 0 && Parents.Peek() != file)
                    Parents.Pop();
            else if ((file = Files.First(f => f.Id == fileId)) != null)
                Parents.Push(file);
            else
                Parents.Clear();

            // Define parameters of request.
            FilesResource.ListRequest listRequest = DataAccess.DriveService.Files.List();
            listRequest.Q = $"\'{fileId}\' in parents";
            listRequest.OrderBy = "folder, name asc";
            listRequest.PageSize = 1000;
            listRequest.Fields = "nextPageToken, files(id, name, parents)";
            // List files.
            Files = listRequest.Execute().Files;
        }
    }
}
//"https://drive.google.com/a/apps.losrios.edu/uc?id=1ikvj5BO71CGiHp3Ccxk6_zPmOdGgPXcT&export=download",
//"webViewLink": "https://drive.google.com/a/apps.losrios.edu/file/d/1ikvj5BO71CGiHp3Ccxk6_zPmOdGgPXcT/view?usp=drivesdk