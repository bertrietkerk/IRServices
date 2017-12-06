using System;
using System.Collections.Generic;

namespace InsuranceRight.Services.Models.Response
{
    public class ReturnObject<T>
    {
        public ReturnObject() { }

        public ReturnObject(List<string> errorList, T @object)
        {
            ErrorMessages = errorList;
            Object = @object;
        }

        public List<string> ErrorMessages { get; set; }
        public T Object { get; set; }
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
