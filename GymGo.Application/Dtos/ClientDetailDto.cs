using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymGo.Application.Dtos
{
    public class ClientDetailDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; }
        public string Address { get; set; } = null!;
        public string UrlImage { get; set; } = null;
        public bool IsActive { get; set; }
    }
}
