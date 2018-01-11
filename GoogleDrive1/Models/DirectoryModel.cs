using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoogleDrive1.Models
{
    public class DirectoryModel
    {
        public string Id { get; set; }
        public List<FileModel> Files { get; set; }
        public Stack<string> Parents = new Stack<string>();

        public static DirectoryModel Instance { get; } = new DirectoryModel();

        private DirectoryModel()
        {
            Parents.Push("root");
        }

        public static void GetDirectory(string folder)
        {

        }
    }
}
