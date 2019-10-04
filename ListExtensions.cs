using System;
using System.Collections.Generic;
using System.Linq;

namespace BugzapperLabs.Temperatured
{
    public static class ListExtensions
    {
        public static double FilteredAverage(this IList<double> list, double standardFactor)
        {
            var mean = list.Average(x => x);
            var d =
                Math.Sqrt(list.Select(x => Math.Pow(x - mean, 2)).Sum() /
                          list.Count);

            double result;
            if (Math.Abs(d) < 1)
                result = list.First();
            else
                result = list.Where(x =>
                        x >
                        mean - standardFactor * d)
                    .Where(x => x <
                                mean + standardFactor * d)
                    .Average(x => x);

            return result;
        }
    }
}