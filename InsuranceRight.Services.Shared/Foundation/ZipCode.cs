using System;
using System.Collections.Generic;
using System.Text;

namespace InsuranceRight.Services.Shared.Models
{
    public class ZipCode
    {
        public string Zipcode { get; set; }
        public override string ToString()
        {
            return Zipcode;
        }
    }
}
