using System;
using Voting.Data.Models.Enums;

namespace Voting.Data.Models
{
    public class User: IEntity
    {
        public long Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public DateTime DateJoined { get; set; }

        public string Email { get; set; }

        public string Salt { get; set; }

        public string Password { get; set; }

        public long? Country { get; set; }

        public string MobileNumber { get; set; }

        public string MobileNumberCode { get; set; }

        public bool MobileNumberConfirmed { get; set; }

        public string ConfirmationToken { get; set; }

        public bool Confirmed { get; set; }

        public DateTime? DateConfirmed { get; set; }

        public DateTime? LastLogin { get; set; }

        public AuthType? AuthType { get; set; } 

        public string AuthID { get; set; }

        public bool Deleted { get; set; }

        public string ForgotPasswordToken { get; set; }

        public DateTime? ForgotPasswordTimeStamp { get; set; }

        public Role Role;

        public string FullName => ((string.IsNullOrWhiteSpace(FirstName) ? "" : FirstName) + " "
                                   + (string.IsNullOrWhiteSpace(MiddleName) ? "" : MiddleName) + " "
                                   + (string.IsNullOrWhiteSpace(LastName) ? "" : LastName)).Trim();

    }
}
