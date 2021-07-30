using Mikrotik.BL.DTOs;
using System;
using System.Web.Http;

namespace Mikrotik.API.Controllers
{
    [RoutePrefix("api/DashboardApi")]
    public class DashboardApiController : ApiController
    {
        [HttpPost]
        public IHttpActionResult GetConnection(ClientDTO clientDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var mikrotikService = new BL.Services.MikrotikService();
                mikrotikService.GetConnection(clientDTO.IP, clientDTO.UserName, string.IsNullOrEmpty(clientDTO.Password) ? string.Empty : clientDTO.Password);
                return Ok();
            }
            catch (Exception ex) { return InternalServerError(ex); }
        }
    }
}
