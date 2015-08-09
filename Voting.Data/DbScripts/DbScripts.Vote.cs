namespace Voting.Data.DbScripts
{
    public partial class DbScripts
    {
        public class Vote
        {
            public static readonly string Insert = @"INSERT INTO Vote(UserId, CandidateId, TimeStamp) 
                                                    VALUES(@UserId, @CandidateId, @TimeStamp)";

            public static readonly string GetUserVote = @"SELECT * FROM Vote v INNER JOIN Candidate c ON v.CandidateId = c.Id 
                                                            WHERE v.UserId = @userId AND c.PositionId = @PositionId";
        }
    }
}
