using System.Linq;
using System.Threading.Tasks;
using Twilio;

namespace Voting.Sms.Impl
{
    public class SmsSender : ISmsSender
    {
        private readonly TwilioRestClient client;
        private readonly string fromPhoneNumber;

        public SmsSender(string accountSid, string authToken, string fromPhoneNumber = null)
        {
            this.client = new TwilioRestClient(accountSid, authToken);

            if (fromPhoneNumber == null)
            {
                fromPhoneNumber = client.ListIncomingPhoneNumbers().IncomingPhoneNumbers.Select(a => a.PhoneNumber).First();
            }

            this.fromPhoneNumber = fromPhoneNumber;
        }

        public Task<bool> Send(string text, string phoneNumber)
        {
            var response = client.SendSmsMessage(fromPhoneNumber, phoneNumber, text);

            if (response.Status == null ||response.Status == "failed" ) return Task.FromResult(false);

            return Task.FromResult(true);
        }
    }
}
