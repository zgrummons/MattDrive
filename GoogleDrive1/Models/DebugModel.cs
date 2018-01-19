using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.Drive.v3.Data;

namespace GoogleDrive1.Models
{
    public class DebugModel
    {
        public QuotaModel Quota;
        public DirectoryModel Directory;
        public long TimeTaken;
    }
}
