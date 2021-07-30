using System;
using System.Linq;
using System.Web.Http;

namespace Mikrotik.API.Controllers
{
    [RoutePrefix("api/ManageBackupApi")]
    public class ManageBackupApiController : ApiController
    {
        [Route("GetFiles")]
        [HttpGet]
        public IHttpActionResult GetFiles()
        {
            try
            {
                var mikrotikService = new BL.Services.MikrotikService();
                var data = mikrotikService.GetFiles().Where(x => x.Name.Contains(".rsc")).ToList();

                return Ok(data);
            }
            catch (Exception ex) { return InternalServerError(ex); }
        }

        [Route("ExportFile")]
        [HttpGet]
        public IHttpActionResult ExportFile()
        {
            try
            {
                var mikrotikService = new BL.Services.MikrotikService();
                mikrotikService.ExportFile();
                return Ok();
            }
            catch (Exception ex) { return InternalServerError(ex); }
        }

        [Route("ImportFile")]
        [HttpGet]
        public IHttpActionResult ImportFile(string id)
        {
            try
            {
                var mikrotikService = new BL.Services.MikrotikService();

                var files = mikrotikService.GetFiles().Where(x => x.Name.Contains(".rsc")).ToList();
                var file = files.FirstOrDefault(x => x.Id.Replace("*", string.Empty).Equals(id));
                mikrotikService.ImportFile(file.Name);
                return Ok();
            }
            catch (Exception ex) { return InternalServerError(ex); }
        }
    }
}
