namespace Voting.Models.Results
{
    public enum ResultCode
    {
        SUCCESS,
        INVALID_PARAMETER,
        USER_NOT_FOUND,
        ALREADY_CONFIRMED,
        EMAIL_NOT_CONFIRMED,
        AUTH_FAILED,
        ALREADY_VOTED,
        UNKNOWN
    }
}
