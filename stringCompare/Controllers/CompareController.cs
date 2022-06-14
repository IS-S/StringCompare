using Microsoft.AspNetCore.Mvc;
using stringCompare.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace stringCompare.Controllers
{
    [ApiController]
    [Route("compare")]
    public class CompareController : ControllerBase
    {
        private readonly ICompareManager _comparemanager;

        public CompareController(ICompareManager comparemanager)
        {
            _comparemanager = comparemanager;
        }

        [HttpPost]
        [Route("strings")]
        public ActionResult<CompareResult> PostStrings(datainStr Data)
        {

            var result = _comparemanager.Compare(Data.stringOne, Data.stringTwo);
            
            return Ok(result);

        }
        [HttpPost]
        [Route("lists")]
        public ActionResult<List<string>> PostLists(datainLst Data)
        {

            List<string> res = new();

            foreach(string strListOne in Data.listOne)
            {
                foreach(string strListTwo in Data.listTwo)
                {
                    var result = _comparemanager.Compare(strListOne, strListTwo);
                    
                    if(result.result == "match")
                    {
                        switch(Data.namesPriorityList)
                        {
                            case 1:
                                if (res.Exists(element => element == strListOne) == false)
                                {
                                    res.Add(strListOne);
                                }
                                break;
                            case 2:
                                if(res.Exists(element => element == strListTwo) == false)
                                {
                                    res.Add(strListTwo);
                                }
                                break;
                            default:
                                if (res.Exists(element => element == strListOne) == false)
                                {
                                    res.Add(strListOne);
                                }
                                break;
                        }
                    }
                }
            }

            if(res.Count == 0)
            {
                return NotFound();
            }
            else
            {
                return Ok(res);
            }
        }
    }

    public class datainStr
    {
        public string stringOne { get; set; }
        public string stringTwo { get; set; }
    }
    public class datainLst
    {
        public List<string> listOne { get; set; }
        public List<string> listTwo { get; set; }
        public int namesPriorityList { get; set; }
    }
}
