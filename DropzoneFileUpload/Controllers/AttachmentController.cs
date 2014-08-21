using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;

namespace DropzoneFileUpload.Controllers
{
    public class AttachmentController : Controller
    {
        [HttpPost]
        public ActionResult Handle()
        {
            var allFiles = new List<TemporaryFileViewModel>();

            var folder = GetTemporaryFolder();
            Directory.CreateDirectory(folder);

            foreach (string fileName in Request.Files.AllKeys)
            {
                var file = Request.Files[fileName];
                if (file == null)
                {
                    continue;
                }

                var targetFilePath = Path.Combine(folder, file.FileName);
                var fileInfo = new FileInfo(targetFilePath);
                if (fileInfo.Exists)
                {
                    // Error
                }

                file.SaveAs(targetFilePath);
                allFiles.Add(new TemporaryFileViewModel()
                             {
                                 ContentType = file.ContentType,
                                 FileName = file.FileName,
                                 FileSize = file.ContentLength,
                                 Path = targetFilePath,
                             });
            }

            return Json(allFiles, JsonRequestBehavior.AllowGet);
        }

        private string GetTemporaryFolder()
        {
            var temporaryFolder = Server.MapPath("~/App_Data/TempFiles");
            var componentId = Request.Headers["X-ComponentId"];

            if (string.IsNullOrWhiteSpace(componentId))
            {
                componentId = Guid.NewGuid().ToString();
            }

            return Path.Combine(temporaryFolder, componentId);
        }
    }
}
