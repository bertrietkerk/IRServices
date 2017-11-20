using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceRight.Services.AddressService.Models
{
    public class ZipCodeDto : ZipCode
    {
        public ZipCodeStatus Status { get; set; }
    }


    public enum ZipCodeStatus
    {
        None,
        Valid,
        EmptyOrNull,
        IncorrectFormat,
        NotFound
    }
}
