using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;

namespace GoogleDrive1.Models
{
    public sealed class QuotaModel
    {
        public bool Unlimited;
        private string _totalSize;
        public string TotalSize
        {
            get
            {
                if ((DateTime.Now - _lastChecked).Hours >= 1) UpdateQuota();
                return _totalSize;
            }
        }
        public string DriveSize;
        public string RecycleBinSize;
        private DateTime _lastChecked;
        private About.StorageQuotaData _quotaData;

        public static QuotaModel Instance { get; } = new QuotaModel();

        private QuotaModel()
        {
            UpdateQuota();
        }

        private void UpdateQuota()
        {
            var about = DataAccess.DriveService.About.Get();
            about.Fields = "storageQuota";
            _quotaData = about.Execute().StorageQuota;
            _lastChecked = DateTime.Now;
            Unlimited = _quotaData.Limit == null;
            _totalSize = FormatSize(_quotaData.Usage);
            DriveSize = FormatSize(_quotaData.UsageInDrive);
            RecycleBinSize = FormatSize(_quotaData.UsageInDriveTrash);
        }

        private static string FormatSize(long? input)
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
