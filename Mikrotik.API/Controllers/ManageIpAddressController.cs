using Mikrotik.BL.DTOs;
using System;
using System.Web.Mvc;

namespace Mikrotik.API.Controllers
{
    public class ManageIpAddressController : Controller
    {
        // GET: ManageIp
        public ActionResult Index()
        {
            try
            {
                var mikrotikService = new BL.Services.MikrotikService();
                ViewBag.IpAddress = mikrotikService.GetIpAddress();
                ViewBag.Route = mikrotikService.GetRoute();

                return View(new AddIpAddressDTO());
            }
            catch (Exception ex)
            {
                ViewBag.errorMessage = ex.Message;
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult Add(AddIpAddressDTO model)
        {
            try
            {
                var mikrotikService = new BL.Services.MikrotikService();

                if (ModelState.IsValid)
                {                    
                    mikrotikService.AddIpAddress(model.Address,
                        model.Interface,
                        model.Network);
                }

                ViewBag.IpAddress = mikrotikService.GetIpAddress();
                ViewBag.Route = mikrotikService.GetRoute();

                return View("Index", model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View("Index", model);
            }
        }
    }
}