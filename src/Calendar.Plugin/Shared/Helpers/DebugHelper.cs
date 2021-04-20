using System;
using System.Diagnostics;

namespace Xamarin.Plugin.Calendar.Helpers
{
    public static class DebugHelper
    {
        public static void MeasureMethodExecutionTime(Action action)
        {
    #if DEBUG
            var sv = Stopwatch.StartNew();

            action.Invoke();

            sv.Stop();
            Debug.WriteLine($"{action.Method.Name} execution time: {sv.Elapsed}");
    #endif
        }
    }
}
