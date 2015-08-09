using System;

namespace Voting.Data.Models
{
    public class Vote:IEntity
    {
        public long Id { get; set; }

        public long UserId { get; set; }

        public long CandidateId { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}
