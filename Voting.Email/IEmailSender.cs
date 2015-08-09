using System.Threading.Tasks;
using Voting.Email.Models;

namespace Voting.Email
{
    public interface IEmailSender
    {
        Task Send(MailBase model);
    }
}
