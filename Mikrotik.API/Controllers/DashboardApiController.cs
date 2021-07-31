using Mikrotik.BL.DTOs;
using System;
using System.Web.Http;

namespace Mikrotik.API.Controllers
{
    [RoutePrefix("api/DashboardApi")]
    public class DashboardApiController : ApiController
    {
        /// <summary>
        /// Metodo para traer los datos del DTO client
        /// </summary>
        /// <param name="clientDTO">Objeto de autenticacion </param>
        /// <returns></returns>
        /// <response code = "200">ok. devuelve el objeto solicitado </response>
        /// <response code = "400">badrequest. no se cumple la validacion del modelo </response>
        /// <response code = "500">internalserver Error. Se ha presentado un error </response>
        /// <summary>    
        [Route("Client")]
        [HttpPost]
        public IHttpActionResult Client(ClientDTO clientDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var mikrotikService = new BL.Services.MikrotikService();
                    mikrotikService.GetConnection(clientDTO.IP, clientDTO.UserName, string.IsNullOrEmpty(clientDTO.Password) ? string.Empty : clientDTO.Password);

                    return Ok();
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        /// <summary>
        /// Metodo para traer los recursos del Router
        /// </summary>
        /// <param name="GetSystemResource">Objeto de autenticacion </param>
        /// <returns></returns>
        /// <response code = "200">ok. devuelve el objeto solicitado </response>
        /// <response code = "400">badrequest. no se cumple la validacion del modelo </response>
        /// <response code = "500">internalserver Error. Se ha presentado un error </response>
        /// <summary>    
        [Route("GetSystemResource")]
        [HttpGet]
        public IHttpActionResult GetSystemResource()
        {
            try
            {
                var mikrotikService = new BL.Services.MikrotikService();
                var systemResource = mikrotikService.GetSystemResource();

                return Ok(systemResource);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        /// <summary>
        /// Metodo para traer las colas
        /// </summary>
        /// <param name="GetQueueSimple">Objeto de autenticacion </param>
        /// <returns></returns>
        /// <response code = "200">ok. devuelve el objeto solicitado </response>
        /// <response code = "400">badrequest. no se cumple la validacion del modelo </response>
        /// <response code = "500">internalserver Error. Se ha presentado un error </response>
        /// <summary>    
        [Route("GetQueueSimple")]
        [HttpGet]
        public IHttpActionResult GetQueueSimple()
        {
            try
            {
                var mikrotikService = new BL.Services.MikrotikService();
                var queueSimple = mikrotikService.GetQueueSimple();

                return Ok(queueSimple);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        /// <summary>
        /// Metodo para traer las interfaces
        /// </summary>
        /// <param name="GetInterface">Objeto de autenticacion </param>
        /// <returns></returns>
        /// <response code = "200">ok. devuelve el objeto solicitado </response>
        /// <response code = "400">badrequest. no se cumple la validacion del modelo </response>
        /// <response code = "500">internalserver Error. Se ha presentado un error </response>
        /// <summary>    
        [Route("GetInterface")]
        [HttpGet]
        public IHttpActionResult GetInterface()
        {
            try
            {
                var mikrotikService = new BL.Services.MikrotikService();
                var interfaces = mikrotikService.GetInterface();

                return Ok(interfaces);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        /// <summary>
        /// Metodo para traer las interfaces
        /// </summary>
        /// <param name="GetIpAddress">Objeto de autenticacion </param>
        /// <returns></returns>
        /// <response code = "200">ok. devuelve el objeto solicitado </response>
        /// <response code = "400">badrequest. no se cumple la validacion del modelo </response>
        /// <response code = "500">internalserver Error. Se ha presentado un error </response>
        /// <summary>    
        [Route("GetIpAddress")]
        [HttpGet]
        public IHttpActionResult GetIpAddress()
        {
            try
            {
                var mikrotikService = new BL.Services.MikrotikService();
                var ipAddress = mikrotikService.GetIpAddress();

                return Ok(ipAddress);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        /// <summary>
        /// Metodo para traer las rutas del router
        /// </summary>
        /// <param name="GetRoute">Objeto de autenticacion </param>
        /// <returns></returns>
        /// <response code = "200">ok. devuelve el objeto solicitado </response>
        /// <response code = "400">badrequest. no se cumple la validacion del modelo </response>
        /// <response code = "500">internalserver Error. Se ha presentado un error </response>
        /// <summary>    
        [Route("GetRoute")]
        [HttpGet]
        public IHttpActionResult GetRoute()
        {
            try
            {
                var mikrotikService = new BL.Services.MikrotikService();
                var routes = mikrotikService.GetRoute();
                return Ok(routes);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        /// <summary>
        /// Metodo para traer los archivos
        /// </summary>
        /// <param name="GetFiles">Objeto de autenticacion </param>
        /// <returns></returns>
        /// <response code = "200">ok. devuelve el objeto solicitado </response>
        /// <response code = "400">badrequest. no se cumple la validacion del modelo </response>
        /// <response code = "500">internalserver Error. Se ha presentado un error </response>
        /// <summary>    
        [Route("GetFiles")]
        [HttpGet]
        public IHttpActionResult GetFiles()
        {
            try
            {
                var mikrotikService = new BL.Services.MikrotikService();
                var files = mikrotikService.GetFiles();
                return Ok(files);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
