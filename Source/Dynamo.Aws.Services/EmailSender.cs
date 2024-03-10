using Amazon.SimpleEmail;
using Dynamo.Business.Shared.Utilities;
using Amazon.SimpleEmail.Model;

namespace Dynamo.Aws.Services
{
    public class EmailSender : IEmailSender
    {
        public void SendEmail(IEnumerable<string> toAddresses, string fromAddress, string subject, string body)
        {
            // Create an SES client
            //using (var client = new AmazonSimpleEmailServiceClient(awsAccessKey, awsSecretKey, region))
            using (var client = new AmazonSimpleEmailServiceClient())
            {
                // Create the destination and message objects
                var destination = new Destination(toAddresses.ToList());
                var message = new Message(new Content(subject), new Body(new Content(body)));

                // Create the request
                var sendRequest = new SendEmailRequest(fromAddress, destination, message);

                try
                {
                    // Send the email
                    //var response = await client.SendEmailAsync(sendRequest);
                    var response = client.SendEmailAsync(sendRequest).Result;
                    Console.WriteLine($"Email sent! Message ID: {response.MessageId}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error sending email: {ex.Message}");
                }
            }
        }
    }
}
