using System;

namespace InsuranceRight.Services.Shared.Models
{
    public class ReturnObject<T>
    {
        public ReturnObject() { }

        public ReturnObject(bool isValid, string message, T @object)
        {
            IsValid = isValid;
            Message = message;
            Object = @object;
        }

        public bool IsValid { get; set; }
        public string Message { get; set; }
        public T Object { get; set; }
    }
}
