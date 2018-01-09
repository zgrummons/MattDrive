using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace GoogleDrive1.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            var quota = Models.DataAccess.GetQuota().Result;

            return View();
        }
    }
}