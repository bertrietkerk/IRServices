using InsuranceRight.Services.Models.Foundation;
using System;
using System.Collections.Generic;
using System.Text;

namespace InsuranceRight.Services.Models.ViewModels
{
    public class PersonViewModel
    {

        public PersonViewModel()
        {
            ResidenceAddress = new Address();
            CorrespondenceAddress = new Address();
        }

        public virtual bool IsSelfEmployed { get; set; }

        public virtual string Sex { get; set; }

        public virtual string FirstName { get; set; }

        public virtual string LastName { get; set; }

        public DateTime? BirthDate { get; set; }

        public virtual string SocialSecurityNumber { get; set; }

        public virtual string HaveIncome { get; set; }

        public virtual string OrganisationName { get; set; }

        public virtual string OrganisationID { get; set; }

        public virtual string PhoneNumber { get; set; }

        public virtual string EmailAddress { get; set; }

        public virtual Address ResidenceAddress { get; set; }

        public virtual bool IsCorrespondenceAddressDifferent { get; set; }

        public virtual Address CorrespondenceAddress { get; set; }
    }
}
