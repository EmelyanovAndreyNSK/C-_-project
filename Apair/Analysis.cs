using System;
using System.Collections.Generic;
using System.Linq;

namespace Delegates.PairsAnalysis
{
    public static class Analysis
    {
        public static int FindMaxPeriodIndex(params DateTime[] data)
            => data
                .Pairs()
                .Select(val => (val.Item2 - val.Item1).TotalSeconds)
                .MaxIndex();

        public static double FindAverageRelativeDifference(params double[] data)
            => data
                .Pairs()
                .Select(val => ((val.Item2 - val.Item1) / val.Item1))
                .Average();
    }

    public static class Extensions
    {
        public static IEnumerable<Tuple<T, T>> Pairs<T>(this IEnumerable<T> list)
        {
            var first = true;
            var previous = default(T);
            foreach (var value in list)
                if (first)
                {
                    previous = value;
                    first = false;
                }
                else
                {
                    yield return Tuple.Create(previous, value);
                    previous = value;
                }
        }

        public static int MaxIndex<T>(this IEnumerable<T> list) where T : IComparable
        {
            var index = 0;
            var maxIndex = 0;
            var max = default(T);

            //var enumerator = list.GetEnumerator();
            foreach(var enumerator in list)
            ///*while (enumerator.MoveNext())
            {
                if (enumerator.CompareTo(max) > 0)
                {
                    max = enumerator;
                    maxIndex = index;
                }
                index++;
            }
            if (index == 0)
                throw new InvalidOperationException();
            return maxIndex;
        }

        private static T MinValue<T>(this Type self)
            => (T)self.GetField(nameof(MinValue)).GetRawConstantValue();
    }
}