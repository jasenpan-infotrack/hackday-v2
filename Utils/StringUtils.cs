using System;

namespace DocumentVerification.Utils
{
    public static class StringUtils
    {
        public static bool ContainsIgnoreCase(this string s, string t)
        {
            if (s.IndexOf(t, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return true;
            }
            return false;
        }
    }
}
