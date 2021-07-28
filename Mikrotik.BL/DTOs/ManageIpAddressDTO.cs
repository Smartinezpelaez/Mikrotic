using System.ComponentModel.DataAnnotations;

namespace Mikrotik.BL.DTOs
{
    public class AddIpAddressDTO
    {        
        [Required]
        public string Address { get; set; }

        [Required]
        public string Interface { get; set; }

        [Required]
        public string Network { get; set; }
    }
}
