using System;
using System.Threading.Tasks;

namespace stringCompare.Core
{
    public class CompareEvaluation : ICompareEvaluation
    {
        private readonly ICompareProcessor _compareprocessor;

        public CompareEvaluation(ICompareProcessor compareProcessor)
        {
            _compareprocessor = compareProcessor;
        }
        public Tuple<string,double> EvaluateResult(string sOne, string sTwo)
        {

            var result = _compareprocessor.Prox(sOne.ToLower(), sTwo.ToLower(), null);

            if (result > 0.85)
            {
                return Tuple.Create("match",result);
            }
            else
            {
                return Tuple.Create("mismatch", result);
            }

        }

    }
}
