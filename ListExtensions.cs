using System;
using System.Collections.Generic;
using System.Linq;

namespace BugzapperLabs.Temperatured
{
    public static class ListExtensions
    {
        /// <summary>
        /// Calculates the filtered average of the values in the list.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="standardFactor">The smaller the number, the more strict the filter will be.</param>
        /// <returns></returns>
        public static double FilteredAverage(this IList<double> list, double standardFactor)
        {
            if (!list.Any())
            {
                return double.MinValue;
            }
            var mean = list.Average(x => x);
            var standardDeviation =
                Math.Sqrt(list.Select(x => Math.Pow(x - mean, 2)).Sum() /
                          list.Count);

            double result;
            if (Math.Abs(standardDeviation) < 1)
                result = list.First();
            else
            {
                result = list.Where(x =>
                        x >
                        mean - standardFactor * standardDeviation)
                    .Where(x => x <
                                mean + standardFactor * standardDeviation)
                    .Average(x => x);
            }

            return result;
        }

        public static void EnsureMaxCapacity(this IList<double> list, int maxCapacity)
        {
            while (list.Count > maxCapacity)
            {
                list.RemoveAt(0);
            }
        }
    }
}