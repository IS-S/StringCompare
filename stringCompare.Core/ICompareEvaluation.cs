using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stringCompare.Core
{
    public interface ICompareEvaluation
    {
        public Tuple<string, double> EvaluateResult(string sOne, string sTwo);
    }
}
