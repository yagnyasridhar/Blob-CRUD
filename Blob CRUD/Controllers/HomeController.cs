using Blob_CRUD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blob_CRUD.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase uploadFile)
        {
            foreach (string file in Request.Files)
            {
                uploadFile = Request.Files[file];
            }
            // Container Name - picture  
            BlobManager BlobManagerObj = new BlobManager();
            string FileAbsoluteUri = BlobManagerObj.UploadFile(uploadFile);
            return RedirectToAction("Get");
        }

        public ActionResult Get()
        {
            // Container Name - picture  
            BlobManager BlobManagerObj = new BlobManager();
            List<string> fileList = BlobManagerObj.GetAll();
            return View(fileList);
        }

        public ActionResult Delete(string uri)
        {
            // Container Name - picture  
            BlobManager BlobManagerObj = new BlobManager();
            BlobManagerObj.Delete(uri);
            return RedirectToAction("Get");
        }
    }
}