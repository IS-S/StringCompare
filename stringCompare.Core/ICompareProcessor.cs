using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stringCompare.Core
{
    public interface ICompareProcessor
    {
        public double Prox(string firstWord, string secondWord, IEqualityComparer<char> comparer = null);
    }
}
