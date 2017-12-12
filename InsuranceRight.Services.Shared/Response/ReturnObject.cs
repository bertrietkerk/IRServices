using System;
using System.Collections.Generic;

namespace InsuranceRight.Services.Models.Response
{
    public class ReturnObject<T>
    {
        public ReturnObject() {
            ErrorMessages = new List<string>();
        }

        public ReturnObject(List<string> errorList, T @object)
        {
            ErrorMessages = errorList;
            Object = @object;
        }

        /// <summary>
        /// A list of error messages for the request
        /// </summary>
        public List<string> ErrorMessages { get; set; }

        /// <summary>
        /// Object of type <typeparamref name="T"/> 
        /// </summary>
        /// <typeparam name="T">The type of the returned object</typeparam>
        public T Object { get; set; }

        /// <summary>
        /// Boolean indicating if there are errors (true) or not (false). Get possible errors from the <c>ErrorMessages</c> property
        /// </summary>
        public bool HasErrors
        {
            get
            {
                if (ErrorMessages.Count == 0)
                {
                    return false;
                }
                return true;
            }
        }
    }
}
