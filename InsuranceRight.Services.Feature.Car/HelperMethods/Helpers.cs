using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InsuranceRight.Services.Feature.Car.HelperMethods
{
    public static class Helpers
    {


        // TODO: make proper method for checking multiple nested objects for null
        private static string GetTypeName<T>(T obj)
        {
            return typeof(T).Name;
        }

        private static string GetTypeName(object obj)
        {
            if (obj != null)
            {
                return obj.GetType().Name;
            }

            return "null";
        }

        /// <summary>
        /// Check the given objects for null
        /// </summary>
        /// <param name="objects">Objects to check for null</param>
        /// <returns>Dictionary pair. Key: given object, Bool: true if obj is null</returns>
        public static Dictionary<string, bool> IsObjectNull(params object[] objects)
        {
            var dict = new Dictionary<string, bool>();
            foreach (var obj in objects)
            {
                var key = GetTypeName(obj);
                var value = false;
                if (obj == null)
                {
                    value = true;
                }
                dict.Add(key, value);
            }
            return dict;
        }

        /// <summary>
        /// Check the given objects for null
        /// </summary>
        /// <param name="objects">Objects to check for null</param>
        /// <returns>True if any object is null</returns>
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


        //public static int GetDriverAge(string ageRange)
        //{
        //    var driverAge = 99;

        //    if (!string.IsNullOrWhiteSpace(ageRange))
        //    {
        //        var ageRangeArray = ageRange.Split(new[] { "-", "+", " " }, StringSplitOptions.RemoveEmptyEntries);
        //        if (ageRangeArray.Length > 0)
        //        {
        //            int ageRangeLast;
        //            if (int.TryParse(ageRangeArray.LastOrDefault().Trim(), out ageRangeLast))
        //            {
        //                driverAge = Math.Min(driverAge, ageRangeLast);
        //            };
        //        }
        //    }
        //    return driverAge;
        //}

        public static int CalculateDriverAge(DateTime? dateOfBirth)
        {
            return CalculateDriverAge(dateOfBirth, DateTime.Now);
        }

        public static int CalculateDriverAge(DateTime? dateOfBirth, DateTime reference)
        {
            if (!dateOfBirth.HasValue)
            {
                return 99;
            }

            int age = reference.Year - dateOfBirth.Value.Year;
            if (reference < dateOfBirth.Value.AddYears(age))
                age--;
            return age;
        }
    }
}
