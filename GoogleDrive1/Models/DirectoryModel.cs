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
        public Dictionary<string, File> Parents = new Dictionary<string, File>();

        public static DirectoryModel Instance { get; } = new DirectoryModel();

        private DirectoryModel()
        {            
        }

        public void GetDirectory(string fileId)
        {
            File file = Parents[fileId] ?? GetFile(fileId);

            // Define parameters of request.
            FilesResource.ListRequest listRequest = DataAccess.DriveService.Files.List();
            listRequest.Q = $"\'{fileId}\' in parents";
            listRequest.OrderBy = "folder, name asc";
            listRequest.PageSize = 1000;
            listRequest.Fields = "nextPageToken, files(id, name, parents)";
            // List files.
            Files = listRequest.Execute().Files;
        }

        public File GetFile(string fileId)
        {
            FilesResource.GetRequest getRequest = DataAccess.DriveService.Files.Get(fileId);
            getRequest.Fields = "id,modifiedTime,name,parents,size";
            return getRequest.Execute();
        }
    }
}
//"https://drive.google.com/a/apps.losrios.edu/uc?id=1ikvj5BO71CGiHp3Ccxk6_zPmOdGgPXcT&export=download",
//"webViewLink": "https://drive.google.com/a/apps.losrios.edu/file/d/1ikvj5BO71CGiHp3Ccxk6_zPmOdGgPXcT/view?usp=drivesdk