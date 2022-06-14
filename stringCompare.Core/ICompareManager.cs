using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stringCompare.Core
{
    public interface ICompareManager
    {
        public CompareResult Compare(string sOne, string sTwo);
    }
}
