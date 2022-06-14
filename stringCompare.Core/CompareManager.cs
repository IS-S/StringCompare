using System.Text.Json;
using System.Threading.Tasks;

namespace stringCompare.Core
{
    
    public class CompareManager : ICompareManager
    {

        public CompareResult resultObj = new();
        private readonly ICompareEvaluation _compareevaluation;
        


        public CompareManager(ICompareEvaluation compareEvaluation)
        {
            _compareevaluation = compareEvaluation;
        }

        public CompareResult Compare(string sOne, string sTwo)
        {

            var res = _compareevaluation.EvaluateResult(sOne, sTwo);

            resultObj = JsonSerializer.Deserialize<CompareResult>("{\"result\":\"" + res.Item1 + "\",\"matchDegree\":" + res.Item2 + "}");

            return resultObj;
        }

    }
}
