﻿using Mikrotik.BL.DTOs;
using System;
using System.Web.Http;

namespace Mikrotik.API.Controllers
{
    [RoutePrefix("DashboardApi")]
    public class DashboardApiController : ApiController
    {
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