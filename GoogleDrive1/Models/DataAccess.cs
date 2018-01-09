using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace GoogleDrive1.Models
{
    public static class DataAccess
    {
        private static string _token;
        private static HttpClient _httpClient = new HttpClient();
        private static string _baseAddress = "https://www.googleapis.com/drive/v3/";
        private static string _apiKey = "AIzaSyAGQPELn4glf4MS2B4VrPcAc-E_fXS6Nm8";

        public static DirectoryModel GetDirectory()
        {
            throw new NotImplementedException();
        }

        public static DirectoryModel GetDirectory(string id)
        {
            throw new NotImplementedException();
        }

        public static async Task<QuotaModel> GetQuota()
        {
            HttpResponseMessage responseMessage = await _httpClient.GetAsync(new Uri($"{_baseAddress}about?fields=storageQuota&key={_apiKey}"));
            QuotaModel quotaModel = null;
            if (responseMessage.IsSuccessStatusCode)
                quotaModel = await responseMessage.Content.ReadAsAsync<QuotaModel>();

            return quotaModel;
        }

        //about?fields=appInstalled%2CexportFormats%2CfolderColorPalette%2CimportFormats%2Ckind%2CmaxImportSizes%2CmaxUploadSize%2CstorageQuota%2CteamDriveThemes%2Cuser&key="
    }
}
