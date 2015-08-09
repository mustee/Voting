namespace Voting.Email.Models
{
    public abstract class MailBase
    {
        public MailBase(string subject, string to)
        {
            Subject = subject;
            To = to;
        }

        public string To { get; }

        public string Subject { get; }

        public abstract string Message();
    }
}
