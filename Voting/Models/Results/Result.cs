namespace Voting.Models.Results
{
    public class Result
    {
        public Result(ResultCode code, string description)
        {
            ResultCode = code;
            Description = description;
        }

        public ResultCode ResultCode { get; }

        public string Description { get; }
    }
}
