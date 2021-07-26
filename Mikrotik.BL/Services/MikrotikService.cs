using Mikrotik.BL.DTOs;
using MikrotikDotNet;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mikrotik.BL.Services
{
    public class MikrotikService
    {
        private static MKConnection connection;

        /// <summary>
        /// GetConnection
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        public void GetConnection(string ip,
            string userName,
            string password)
        {
            try
            {
                connection = new MKConnection(ip, userName, password);
                connection.Open();
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// ExecuteCommand
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public List<Dictionary<string, string>> ExecuteCommand(string command)
        {
            try
            {
                if (connection == null)
                    throw new Exception("MKConnection is null");

                var data = new List<Dictionary<string, string>>();
                var cmd = connection.CreateCommand(command);
                var result = cmd.ExecuteReader();

                //only test
                //var result = new List<string> {
                //    "!re=.id=*2=name=antonio=target=192.168.120.251/32=parent=none=packet-marks==priority=8/8=queue=default-small/default-small=limit-at=0/0=max-limit=1000000/128000=burst-limit=0/0=burst-threshold=0/0=burst-time=0s/0s=bucket-size=0.1/0.1=bytes=0/0=total-bytes=0=packets=0/0=total-packets=0=dropped=0/0=total-dropped=0=rate=0/0=total-rate=0=packet-rate=0/0=total-packet-rate=0=queued-packets=0/0=total-queued-packets=0=queued-bytes=0/0=total-queued-bytes=0=invalid=false=dynamic=false=disabled=false",
                //    "!re=.id=*3=name=valeria=target=192.168.120.254/32=parent=none=packet-marks==priority=8/8=queue=default-small/default-small=limit-at=0/0=max-limit=512000/128000=burst-limit=0/0=burst-threshold=0/0=burst-time=0s/0s=bucket-size=0.1/0.1=bytes=0/0=total-bytes=0=packets=0/0=total-packets=0=dropped=0/0=total-dropped=0=rate=0/0=total-rate=0=packet-rate=0/0=total-packet-rate=0=queued-packets=0/0=total-queued-packets=0=queued-bytes=0/0=total-queued-bytes=0=invalid=false=dynamic=false=disabled=false",
                //    "!re=.id=*4=name=juan=target=10.10.128.251/32=parent=none=packet-marks==priority=8/8=queue=default-small/default-small=limit-at=0/0=max-limit=1000000/256000=burst-limit=0/0=burst-threshold=0/0=burst-time=0s/0s=bucket-size=0.1/0.1=bytes=0/0=total-bytes=0=packets=0/0=total-packets=0=dropped=0/0=total-dropped=0=rate=0/0=total-rate=0=packet-rate=0/0=total-packet-rate=0=queued-packets=0/0=total-queued-packets=0=queued-bytes=0/0=total-queued-bytes=0=invalid=false=dynamic=false=disabled=false"
                //};

                foreach (var line in result)
                {
                    var dictionaryData = new Dictionary<string, string>();
                    var splitData = line.Replace("!re=", string.Empty).Split('=');
                    for (int i = 0; i < splitData.Length; i += 2)
                        dictionaryData.Add(splitData[i], splitData[i + 1]);

                    data.Add(dictionaryData);
                }

                return data;
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// GetQueueSimple
        /// </summary>
        /// <returns></returns>
        public List<QueueSimpleDTO> GetQueueSimple()
        {
            var data = new List<QueueSimpleDTO>();
            var resultCommand = ExecuteCommand("/queue/simple/print");
            foreach (var item in resultCommand)
            {
                var speed = item["max-limit"].Split('/').Select(x => Convert.ToInt32(x)).ToList();
                data.Add(new QueueSimpleDTO
                {
                    Id = item[".id"],
                    Name = item["name"],
                    Target = item["target"].Split('/')[0],
                    Up = (speed.FirstOrDefault() / 1000).ToString(),
                    Down = (speed.LastOrDefault() / 1000).ToString(),
                });
            }

            return data;
        }

        /// <summary>
        /// GetSystemResource
        /// </summary>
        /// <returns></returns>
        public SystemResourceDTO GetSystemResource()
        {
            var data = new SystemResourceDTO();
            var resultCommand = ExecuteCommand("/system/resource/print");

            foreach (var item in resultCommand)
            {
                data = new SystemResourceDTO()
                {
                    BoardName = item["board-name"],
                    Uptime = item["uptime"],
                    CpuLoad = item["cpu-load"],
                    Version = item["version"]
                };
            }

            return data;
        }

        /// <summary>
        /// GetInterface
        /// </summary>
        /// <returns></returns>
        public List<InterfaceDTO> GetInterface()
        {
            var data = new List<InterfaceDTO>();
            var resultCommand = ExecuteCommand("/interface/print");

            foreach (var item in resultCommand)
            {
                data.Add(new InterfaceDTO()
                {
                    Id = item[".id"],
                    Type = item["type"],
                    Name = item["name"],
                    MTU = item["mtu"]
                });
            }

            return data;
        }

        /// <summary>
        /// GetRoute
        /// </summary>
        /// <returns></returns>
        public List<RouteDTO> GetRoute()
        {
            var data = new List<RouteDTO>();
            var resultCommand = ExecuteCommand("/ip/route/print");

            foreach (var item in resultCommand)
            {
                data.Add(new RouteDTO()
                {
                    Id = item[".id"],
                    DstAddress = item["dst-address"].Split('/')[0],
                    Gateway = item["gateway"],
                    Distance = item["distance"]
                });
            }

            return data;
        }
    }
}
