using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Http;

namespace CodeMasteryDemo.Controllers
{
    public class RegexController : ApiController
    {
        public IEnumerable<dynamic> Get(string regex, string target)
        {
            var expression = new Regex(regex, RegexOptions.Compiled);
            

            var list = new List<dynamic>();
            int matchCount = 0;
            var targetLines = target.Split(new[]{'\r','\n'}, StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in targetLines)
            {
                var match = expression.Match(line);
                while (match.Success)
                {
                    dynamic matchResult = new {Index = matchCount++, Groups = new List<dynamic>()};
                    for (int groupIndex = 0; groupIndex < match.Groups.Count; groupIndex++)
                    {
                        var group = match.Groups[groupIndex];
                        dynamic groupResult = new {Index = groupIndex, Captures = new List<dynamic>()};
                        for (int captureIndex = 0; captureIndex < group.Captures.Count; captureIndex++)
                        {
                            var capture = group.Captures[captureIndex];
                            groupResult.Captures.Add(new
                            {
                                Index = captureIndex,
                                Position = capture.Index,
                                Value = capture.ToString()
                            });
                        }
                        matchResult.Groups.Add(groupResult);
                    }
                    list.Add(matchResult);

                    match = match.NextMatch();
                }
            }
            return list;
        }
    }
}
