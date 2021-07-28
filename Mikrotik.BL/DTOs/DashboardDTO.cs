namespace Mikrotik.BL.DTOs
{    
    public class QueueSimpleDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Target { get; set; }
        public string Up { get; set; }
        public string Down { get; set; }
    }

    public class SystemResourceDTO
    {
        public string BoardName { get; set; }
        public string Uptime { get; set; }
        public string CpuLoad { get; set; }
        public string Version { get; set; }
    }

    public class InterfaceDTO
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string MTU { get; set; }
    }

    public class RouteDTO
    {
        public string Id { get; set; }
        public string DstAddress { get; set; }
        public string Gateway { get; set; }
        public string Distance { get; set; }
    }

    public class IpAddressDTO
    {
        public string Id { get; set; }
        public string Address { get; set; }
        public string Network { get; set; }
        public string Interface { get; set; }
    }

    public class FileDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Size { get; set; }
        public string CreationTime { get; set; }
    }
}