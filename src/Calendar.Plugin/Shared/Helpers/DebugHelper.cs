using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Xamarin.Plugin.Calendar.Helpers
{
    /// <summary>
    /// Debugging helper class
    /// </summary>
    public static class DebugHelper
    {
        private static Dictionary<string, int> CalledMethodsWithCount { get; set; } = new Dictionary<string, int>();

        /// <summary>
        /// Method to measure execution time of given method
        /// </summary>
        /// <param name="action">Method to evaluate</param>
        /// <param name="additionalLabel">String to add to beginning of debug output</param>
        public static void PrintDebugInfo(Action action, string additionalLabel = "")
        {
#if DEBUG
            var elapsedTime = MeasureExecutionTime(action);
            var timesExecuted = CountTimesExecuted(action);

            Debug.WriteLine($"\n {additionalLabel} {action.Method.Name.ToUpper()} execution time: {elapsedTime}\nTimes executed: {timesExecuted}\n");
#endif
        }

#if DEBUG
        private static TimeSpan MeasureExecutionTime(Action action)
        {
            var sv = Stopwatch.StartNew();

            action.Invoke();

            sv.Stop();

            return sv.Elapsed;
        }

        private static int CountTimesExecuted(Action action)
        {
            if (CalledMethodsWithCount.ContainsKey(action.Method.Name))
                CalledMethodsWithCount[action.Method.Name]++;
            else
                CalledMethodsWithCount[action.Method.Name] = 1;

            return CalledMethodsWithCount[action.Method.Name];
        }
#endif
    }
}
