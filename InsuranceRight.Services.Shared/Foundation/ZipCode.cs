using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InsuranceRight.Services.Shared.Models
{
    public class ZipCode
    {
        [RegularExpression("^[1-9][0-9]{3}[A-Z]{2}$")]
        public string Zipcode { get; set; }

        public override string ToString()
        {
            return Zipcode;
        }
    }
}
