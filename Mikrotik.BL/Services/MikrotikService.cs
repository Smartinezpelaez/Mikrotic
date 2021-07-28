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
        /// ExecuteReader
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public List<Dictionary<string, string>> ExecuteReader(string command)
        {
            try
            {
                if (connection == null)
                    throw new Exception("MKConnection is null");

                var data = new List<Dictionary<string, string>>();
                var cmd = connection.CreateCommand(command);
                var result = cmd.ExecuteReader();

                foreach (var line in result)
                {
                    var dictionaryData = new Dictionary<string, string>();
                    var splitData = line.Replace("!re=", string.Empty).Split('=');
                    for (int i = 0; i < splitData.Length; i += 2)
                    {
                        if (splitData[i].Contains("contents"))
                            break;
                        dictionaryData.Add(splitData[i], splitData[i + 1]);
                    }

                    data.Add(dictionaryData);
                }

                return data;
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// ExecuteNonQuery
        /// </summary>
        /// <param name="command"></param>
        /// <param name="parameters"></param>
        public void ExecuteNonQuery(string command,
            Dictionary<string, string> parameters)
        {
            try
            {
                if (connection == null)
                    throw new Exception("MKConnection is null");

                var cmd = connection.CreateCommand(command);
                foreach (var item in parameters)
                    cmd.Parameters.Add(item.Key, item.Value);

                cmd.ExecuteNonQuery();
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
            var resultCommand = ExecuteReader("/queue/simple/print");
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
            var resultCommand = ExecuteReader("/system/resource/print");

            foreach (var item in resultCommand)
            {
                data = new SystemResourceDTO
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
            var resultCommand = ExecuteReader("/interface/print");

            foreach (var item in resultCommand)
            {
                data.Add(new InterfaceDTO
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
            var resultCommand = ExecuteReader("/ip/route/print");

            foreach (var item in resultCommand)
            {
                data.Add(new RouteDTO
                {
                    Id = item[".id"],
                    DstAddress = item["dst-address"].Split('/')[0],
                    Gateway = item["gateway"],
                    Distance = item["distance"]
                });
            }

            return data;
        }

        /// <summary>
        /// GetIpAddress
        /// </summary>
        /// <returns></returns>
        public List<IpAddressDTO> GetIpAddress()
        {
            var data = new List<IpAddressDTO>();
            var resultCommand = ExecuteReader("/ip/address/print");

            foreach (var item in resultCommand)
            {
                data.Add(new IpAddressDTO
                {
                    Id = item[".id"],
                    Address = item["address"].Split('/')[0],
                    Network = item["network"],
                    Interface = item["interface"]
                });
            }

            return data;
        }

        /// <summary>
        /// AddIpAddress
        /// </summary>
        /// <param name="address"></param>
        /// <param name="interfaceAddress"></param>
        /// <param name="network"></param>
        public void AddIpAddress(string address,
            string interfaceAddress,
            string network)
        {            
            var parameters = new Dictionary<string, string>
            {
                { "address", address },
                { "interface", interfaceAddress },
                { "network", network },
            };
            ExecuteNonQuery("ip address add", parameters);
        }

        /// <summary>
        /// GetFile
        /// </summary>
        /// <returns></returns>
        public List<FileDTO> GetFiles()
        {
            var data = new List<FileDTO>();
            var resultCommand = ExecuteReader("/file/print");

            foreach (var item in resultCommand)
            {
                data.Add(new FileDTO
                {
                    Id = item[".id"],
                    Name = item["name"],
                    Type = item["type"],
                    Size = item.ContainsKey("size") ? item["size"] : string.Empty,
                    CreationTime = item["creation-time"]
                });
            }

            return data;
        }

        /// <summary>
        /// ExportFile
        /// </summary>
        public void ExportFile()
        {
            var parameters = new Dictionary<string, string>
            {
                { "file", string.Format("bck{0}", DateTime.UtcNow.ToString("yyyyMMddHHmmss")) }
            };
            ExecuteNonQuery("export", parameters);
        }

        /// <summary>
        /// ImportFile
        /// </summary>
        /// <param name="fileName"></param>
        public void ImportFile(string fileName)
        {           
            var parameters = new Dictionary<string, string>
            {
                { "file", fileName }
            };
            ExecuteNonQuery("import", parameters);
        }
    }
}
