using System.Threading.Tasks;

namespace Voting.Sms
{
    public interface ISmsSender
    {
        Task<bool> Send(string text, string phoneNumber);
    }
}
