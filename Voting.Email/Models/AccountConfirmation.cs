using System;

namespace Voting.Email.Models
{
    public class AccountConfirmation: MailBase
    {
        private readonly string confirmationUrl;
        public AccountConfirmation(string subject, string to, string confirmationUrl)
            :base(subject, to)
        {
            this.confirmationUrl = confirmationUrl;
        }
       

        public override string Message()
        {
            return @"<h1>Hello and Welcome to St's Peters Voting!</h1>
                <p>Please click on this link confirm your account</p>
                <a href=" + confirmationUrl + ">" + confirmationUrl + "</a>";
        }
    }
}
