using Dynamo.Config;
using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynamo.Commands.Utilities
{
    public class SendEmailCommand : Command
    {
        public SendEmailCommand() : base("sendemail", "Sends an email")
        {
            var toAddressesOption = CreateToAddressesOption();
            var fromAddressOption = CreateFromAddressOption();
            var subjectOption = CreateSubjectOption();
            var bodyOption = CreateBodyOption();
            this.SetHandler(SendEmail, toAddressesOption, fromAddressOption, subjectOption, bodyOption);
        }

        public void SendEmail(IEnumerable<string> toAddresses, string fromAddress, string subject, string body)
        {
            var message = $"Sending email to {toAddresses.First()} from {fromAddress} with subject {subject}";
        }

        private Option<IEnumerable<string>> CreateToAddressesOption()
        {
            //Adding required option for addresses
            var toAddressesOption = new Option<IEnumerable<string>>(new string[2] { "--to-addresses", "-t" },
                "Comma separated list of addresses to send email to.")
            {
                IsRequired = true,
                Arity = ArgumentArity.OneOrMore
            };
            return toAddressesOption;
        }

        private Option<string> CreateFromAddressOption()
        {
            //Adding required option for from address
            var fromAddresses = new Option<string>(new string[2] { "--from-address", "-f" },
                "From address for email.")
            {
                IsRequired = true,
                Arity = ArgumentArity.OneOrMore
            };
            return fromAddresses;
        }

        private Option<string> CreateSubjectOption()
        {
            var subject = new Option<string>(new string[2] { "--subject", "-s" },
                "Subject for email.")
            {
                IsRequired = true,
                Arity = ArgumentArity.OneOrMore
            };
            return subject;
        }

        private Option<string> CreateBodyOption()
        {
            var body = new Option<string>(new string[2] { "--body", "-b" },
                "Body for email.")
            {
                IsRequired = true,
                Arity = ArgumentArity.OneOrMore
            };
            return body;
        }
    }
}
