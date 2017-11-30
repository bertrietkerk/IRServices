using InsuranceRight.Services.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceRight.Services.AddressService.Json
{
    public class AddressJsonRootObject
    {
        public List<Address> Addresses { get; set; }
    }
}
