

namespace stringCompare.Core
{
//    enum Results
//    {
//        equal,
//        different
//    }
    public class CompareResult : ICompareResult
    {
        public string result { get; set; }
        public double matchDegree { get; set; }
    }
}
