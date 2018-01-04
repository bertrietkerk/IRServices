using System;
using System.Collections.Generic;
using System.Text;

namespace InsuranceRight.Services.Models.Foundation
{
    public class PolicyDocument
    {
        /// <summary>
        /// Identification of the policy document
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        /// Title of the policy document
        /// </summary>
        public virtual string Title { get; set; }

        /// <summary>
        /// Icon path of policy document
        /// </summary>
        public virtual string IconUrl { get; set; }

        /// <summary>
        /// Document url of the policy document
        /// </summary>
        public virtual string DocumentUrl { get; set; }

        /// <summary>
        /// File size of policy document
        /// </summary>
        public virtual string FileSize { get; set; }
    }
}
