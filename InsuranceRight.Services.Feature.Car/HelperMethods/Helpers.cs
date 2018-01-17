using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InsuranceRight.Services.Feature.Car.HelperMethods
{
    public static class Helpers
    {
        public static Dictionary<string, bool> IsObjectNull(params object[] objects)
        {
            var dict = new Dictionary<string, bool>();
            foreach (var obj in objects)
            {
                var key = obj.ToString();
                var value = true;
                if (obj != null)
                {
                    value = false;
                }
                dict.Add(key, value);
            }
            return dict;
        }

        public static bool IsAnyObjectNull(params object[] objects)
        {
            foreach (var obj in objects)
            {
                if (obj == null)
                {
                    return true;
                }
            }
            return false;
        }


        public static int GetDriverAge(string ageRange)
        {
            var driverAge = 99;

            if (!string.IsNullOrWhiteSpace(ageRange))
            {
                var ageRangeArray = ageRange.Split(new[] { "-", "+", " " }, StringSplitOptions.RemoveEmptyEntries);
                if (ageRangeArray.Length > 0)
                {
                    var ageRangeLast = int.Parse(ageRangeArray.LastOrDefault().Trim());
                    driverAge = Math.Min(driverAge, ageRangeLast);
                }
            }
            return driverAge;
        }

        public static int CalculateDriverAge(DateTime dateOfBirth)
        {
            return CalculateDriverAge(dateOfBirth, DateTime.Now);
        }

        public static int CalculateDriverAge(DateTime dateOfBirth, DateTime reference)
        {
            int age = reference.Year - dateOfBirth.Year;
            if (reference < dateOfBirth.AddYears(age))
                age--;
            return age;
        }
    }
}
