using System.Collections.Generic;

namespace Blog.BusinessLayer
{
    public class ResultManagement<T> where T : class
    {
        public List<string> Results { get; set; }
        public T Obj { get; set; }

        public ResultManagement()
        {
            Results = new List<string>();
        }
    }
}