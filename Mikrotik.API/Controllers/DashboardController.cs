using Mikrotik.BL.DTOs;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Mikrotik.API.Controllers
{
    public class DashboardController : Controller
    {
        [HttpGet]
        public ActionResult Client()
        {
            return View(new ClientDTO());
        }

        [HttpPost]
        public ActionResult Client(ClientDTO clientDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var mikrotikService = new BL.Services.MikrotikService();
                    mikrotikService.GetConnection(clientDTO.IP, clientDTO.UserName, string.IsNullOrEmpty(clientDTO.Password) ? string.Empty : clientDTO.Password);

                    return RedirectToAction("Index", "Dashboard");
                }

                return View(clientDTO);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(clientDTO);
            }
        }

        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                var mikrotikService = new BL.Services.MikrotikService();
                ViewBag.SystemResource = mikrotikService.GetSystemResource();
                ViewBag.QueueSimple = mikrotikService.GetQueueSimple();
                ViewBag.QueueSimpleJson = JsonConvert.SerializeObject(mikrotikService.GetQueueSimple());
                ViewBag.Interface = mikrotikService.GetInterface();
                ViewBag.IpAddress = mikrotikService.GetIpAddress();
                ViewBag.Route = mikrotikService.GetRoute();
                ViewBag.File = mikrotikService.GetFiles();

                return View();
            }
            catch (Exception ex)
            {
                ViewBag.errorMessage = ex.Message;
                return View("Error");
            }
        }
    }
}