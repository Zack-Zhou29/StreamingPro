using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamingProcess.Service
{
    public class GroupService : IContentParser
    {
        private readonly List<IContentParser> content;

        public GroupService(List<IContentParser> content)
        {
            this.content = content;
        }


        public int GetScore(int input)
        {
            var count = input + 1;
            var res = content.Sum(x => x.GetScore(count));
            return count + res;
        }
    }

}
