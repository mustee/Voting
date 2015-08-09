using System.Collections.Generic;

namespace Voting.Models.Results
{
    public class ListResult<T>: Result
    {
        public ListResult(IEnumerable<T> items, ResultCode code, string description)
            :base(code, description)
        {
            Items = items;
        }

        public IEnumerable<T> Items { get; }
    }
}
