namespace Voting.Email.Models
{
    public class ForgotPassword : MailBase
    {
        private readonly string forgottenPasswordUrl;
        public ForgotPassword(string subject, string to, string forgottenPasswordUrl) : base(subject, to)
        {
            this.forgottenPasswordUrl = forgottenPasswordUrl;
        }

        public override string Message()
        {
            return @"<p> Please click on the link below to reset your password</p>
                   < a href = " + forgottenPasswordUrl + " > " + forgottenPasswordUrl + " </ a > ";
        }
    }
}
