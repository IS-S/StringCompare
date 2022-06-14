using System;
using System.Collections.Generic;
using System.Threading.Tasks;

//Джаро-Винклер

namespace stringCompare.Core
{
    public class CompareProcessor : ICompareProcessor
    {

        private const double tresholdgain = 0.7;
        private const int numfirstchars = 4;

        public double Prox(string firstString, string secondString, IEqualityComparer<char> comparer = null)
        {
            comparer = comparer ?? EqualityComparer<char>.Default;

            var lLen1 = firstString.Length;
            var lLen2 = secondString.Length;

            var searchRange = Math.Max(0, Math.Max(lLen1, lLen2) / 2 - 1);

            var match1 = new bool[lLen1];
            var match2 = new bool[lLen2];

            var numComm = 0;
            for (var i = 0; i < lLen1; ++i)
            {
                var startval = Math.Max(0, i - searchRange);
                var Endval = Math.Min(i + searchRange + 1, lLen2);
                for (var j = startval; j < Endval; ++j)
                {
                    if (match2[j]) continue;
                    if (!comparer.Equals(firstString[i], secondString[j]))
                        continue;
                    match1[i] = true;
                    match2[j] = true;
                    ++numComm;
                    break;
                }
            }

            if (numComm == 0) return 0.0;

            var numTransphalf = 0;
            var k = 0;
            for (var i = 0; i < lLen1; ++i)
            {
                if (!match1[i]) continue;
                while (!match2[k]) ++k;
                if (!comparer.Equals(firstString[i], secondString[k]))
                    ++numTransphalf;
                ++k;
            }
            var lNumTransposed = numTransphalf / 2;

            double numCommonD = numComm;
            var weightval = (numCommonD / lLen1
                              + numCommonD / lLen2
                              + (numComm - lNumTransposed) / numCommonD) / 3.0;

            if (weightval <= tresholdgain) return weightval;
            var maxval = Math.Min(numfirstchars, Math.Min(firstString.Length, secondString.Length));
            var posval = 0;
            while (posval < maxval && comparer.Equals(firstString[posval], secondString[posval]))
                ++posval;
            if (posval == 0) return weightval;
            return weightval + 0.1 * posval * (1.0 - weightval);
        }
    }
}
