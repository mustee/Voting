

namespace Voting.Data.Models
{
    public class Candidate : IEntity
    {
        public long Id { get; set; }

        public long PositionId { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string FullName => ((string.IsNullOrWhiteSpace(FirstName) ? "" : FirstName) + " "
                                   + (string.IsNullOrWhiteSpace(MiddleName) ? "" : MiddleName) + " "
                                   + (string.IsNullOrWhiteSpace(LastName) ? "" : LastName)).Trim();
    }
}
