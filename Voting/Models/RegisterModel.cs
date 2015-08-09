using Voting.Data.Models.Enums;

namespace Voting.Models
{
    public class RegisterModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public AuthType? AuthType { get; set; }

        public string AuthId { get; set; }

        public bool ConfirmEmail { get; set; }
    }
}
