using System.ComponentModel.DataAnnotations;

namespace Mikrotik.BL.DTOs
{
    public class ClientDTO
    {
        [Required]
        public string IP { get; set; }

        [Required]
        public string UserName { get; set; }

        //[Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
