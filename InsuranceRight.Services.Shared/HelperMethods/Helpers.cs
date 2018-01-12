using System;
using System.Collections.Generic;
using System.Text;

namespace InsuranceRight.Services.Models.HelperMethods
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
    }
}
