using System;
using System.Threading.Tasks;

namespace Xamarin.Plugin.Calendar
{
    internal static class Extensions
    {
        internal static string Capitalize(this string source)
        {
            if (source.Length == 0)
                return source;

            return char.ToUpper(source[0]) + source.Substring(1, source.Length - 1);
        }
    }
}
