using System;
using System.Linq;
using System.Web.Mvc;

namespace Mikrotik.API.Controllers
{
    public class ManageBackupController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                var mikrotikService = new BL.Services.MikrotikService();
                ViewBag.File = mikrotikService.GetFiles().Where(x => x.Name.Contains(".rsc")).ToList();

                return View();
            }
            catch (Exception ex)
            {
                ViewBag.errorMessage = ex.Message;
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult ExportFile()
        {
            try
            {
                var mikrotikService = new BL.Services.MikrotikService();

                mikrotikService.ExportFile();
                return RedirectToAction("Index", "ManageBackup");
            }
            catch (Exception ex)
            {
                ViewBag.errorMessage = ex.Message;
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult ImportFile(string id)
        {
            try
            {
                var mikrotikService = new BL.Services.MikrotikService();

                var files = mikrotikService.GetFiles().Where(x => x.Name.Contains(".rsc")).ToList();
                var file = files.FirstOrDefault(x => x.Id.Replace("*", string.Empty).Equals(id));
                mikrotikService.ImportFile(file.Name);
                return RedirectToAction("Index", "ManageBackup");
            }
            catch (Exception ex)
            {
                ViewBag.errorMessage = ex.Message;
                return View("Error");
            }
        }
    }
}