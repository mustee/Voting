using System.Collections.Generic;

namespace Voting.Models.Results
{
    public class ItemResult<T> : Result
    {
        public ItemResult(T item, ResultCode code, string description)
            :base(code, description)
        {
            Item = item;
        }

        public T Item { get; set; }
    }
}
