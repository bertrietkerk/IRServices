using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceRight.Services.AddressService.Models
{
    public class AddressDto : Address
    {
        public AddressStatus Status { get; set; }
    }

    public enum AddressStatus
    {
        None,
        EmptyOrNull,
        Valid,
        NotFound
    }
}
