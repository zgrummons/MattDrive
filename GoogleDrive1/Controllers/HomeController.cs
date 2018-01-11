using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GoogleDrive1.Models;
using System.Threading;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;

namespace GoogleDrive1.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            Stopwatch sw = Stopwatch.StartNew();


            // Define parameters of request.
            FilesResource.ListRequest listRequest = DataAccess.DriveService.Files.List();
            listRequest.Q = "mimeType=\'application/vnd.google-apps.folder\' and \'root\' in parents";
            listRequest.OrderBy = "name asc";
            listRequest.PageSize = 1000;
            listRequest.Fields = "nextPageToken, files(id, name)";
            // List files.
            IList<Google.Apis.Drive.v3.Data.File> files = listRequest.Execute()
                .Files;

            sw.Stop();

            return View(new DebugModel {Files = files, Quota = QuotaModel.Instance, TimeTaken = sw.ElapsedMilliseconds});
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
