using Mikrotik.BL.DTOs;
using System;
using System.Web.Http;

namespace Mikrotik.API.Controllers
{
    [RoutePrefix("api/ManageIpAddressApi")]
    public class ManageIpAddressApiController : ApiController
    {
        [Route("GetIpAddress")]
        [HttpGet]
        public IHttpActionResult GetIpAddress()
        {
            try
            {
                var mikrotikService = new BL.Services.MikrotikService();
                var data = mikrotikService.GetIpAddress();
                return Ok(data);
            }
            catch (Exception ex) { return InternalServerError(ex); }
        }

        [Route("GetRoute")]
        [HttpGet]
        public IHttpActionResult GetRoute()
        {
            try
            {
                var mikrotikService = new BL.Services.MikrotikService();
                var data = mikrotikService.GetRoute();
                return Ok(data);
            }
            catch (Exception ex) { return InternalServerError(ex); }
        }

        [Route("AddIpAddress")]
        [HttpPost]
        public IHttpActionResult AddIpAddress(AddIpAddressDTO model)
        {
            try
            {
                var mikrotikService = new BL.Services.MikrotikService();

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                mikrotikService.AddIpAddress(model.Address,
                    model.Interface,
                    model.Network);

                return Ok();
            }
            catch (Exception ex) { return InternalServerError(ex); }
        }
    }
}
