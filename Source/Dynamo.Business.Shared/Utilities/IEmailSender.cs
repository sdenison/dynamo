using System.Collections.Generic;

namespace Dynamo.Business.Shared.Utilities
{
    public interface IEmailSender
    {
        void SendEmail(IEnumerable<string> toAddresses, string fromAddress, string subject, string body);
    }
}
