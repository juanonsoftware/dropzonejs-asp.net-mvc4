using System.IO;
using System.Web.Mvc;

namespace DropzoneFileUpload.Controllers
{
    public class AttachmentController : Controller
    {
        [HttpPost]
        public ActionResult Handle()
        {
            foreach (string requestFile in Request.Files)
            {
                var file = Request.Files[requestFile];
                if (file == null)
                {
                    continue;
                }

                var temporaryPath = Server.MapPath("~/App_Data/TempFiles");
                var tempFilePath = Path.Combine(temporaryPath, file.FileName);
                file.SaveAs(tempFilePath);
            }

            return new EmptyResult();
        }
    }
}
