namespace Voting.Data.DbScripts
{
    partial class DbScripts
    {
        public class Candidate
        {
            public static readonly string Select = "SELECT * FROM Candidate WHERE Id = @Id";

            public static readonly string FindByPosition = "SELECT * FROM Candidate WHERE PositionId = @PositionId";
        }
    }
}
