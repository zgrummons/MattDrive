using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;

namespace GoogleDrive1.Models
{
    public class QuotaModel
    {
        public bool Unlimited;
        public string TotalSize;
        public string DriveSize;
        public string RecycleBinSize;

        public QuotaModel(About.StorageQuotaData data)
        {
            Unlimited = data.Limit == null;
            TotalSize = FormatSize(data.Usage);
            DriveSize = FormatSize(data.UsageInDrive);
            RecycleBinSize = FormatSize(data.UsageInDriveTrash);
        }

        private string FormatSize(long? input)
        {
            if (input < 1024)
                return string.Format($"{input} B");
            input /= 1024;
            if (input < 1024)
                return string.Format($"{input} KB");
            input /= 1024;
            if (input < 1024)
                return string.Format($"{input} MB");
            input /= 1024;
            if (input < 1024)
                return string.Format($"{input} GB");
            input /= 1024;
            if (input < 1024)
                return string.Format($"{input} TB");
            input /= 1024;
            if (input < 1024)
                return string.Format($"{input} PB");
            return string.Empty;
        }
    }
}
