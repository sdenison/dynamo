using Dynamo.Business.Shared.Utilities;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Dynamo.Data.DynamoDb.Integration.Utilities
{
    [TestFixture]
    public class PuzzleSubmitterTests
    {
        private HttpClient _httpClient;
        private PuzzleSubmitter _submitter;

        public PuzzleSubmitterTests()
        {
            _httpClient = new HttpClient();
            _submitter = new PuzzleSubmitter(_httpClient);
        }

        [SetUp]
        public void Setup()
        {
            _httpClient = new HttpClient();
            _submitter = new PuzzleSubmitter(_httpClient);
        }

        [Test]
        public async Task Can_submit_correct_answer_to_the_endpoint()
        {
            // Arrange
            var submission = new Submission
            {
                EmailAddress = "mycrazyemail@lmi.org",
                Question = "test",
                Answer = 777.777 // Make sure this is expected by the API
            };

            // Act
            Response result = await _submitter.SubmitAnswerAsync(submission);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.EmailAddress, Is.EqualTo(submission.EmailAddress));
            Assert.That(result.Question, Is.EqualTo(submission.Question));
            Assert.That(result.Answer, Is.EqualTo(submission.Answer));
            Assert.That(result.Result, Is.EqualTo("correct"));
            Assert.That(result.DateTime, Is.Not.EqualTo(default(DateTime)));
            Assert.That(result.IdTime, Is.Not.Null.And.Not.Empty);

            // Optional output for debugging
            TestContext.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
        }

        [Test]
        public async Task Can_submit_answer_that_is_too_high_to_the_endpoint()
        {
            // Arrange
            var submission = new Submission
            {
                EmailAddress = "mycrazyemail@lmi.org",
                Question = "test",
                Answer = 777.779 // Make sure this is expected by the API
            };

            // Act
            Response result = await _submitter.SubmitAnswerAsync(submission);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.EmailAddress, Is.EqualTo(submission.EmailAddress));
            Assert.That(result.Question, Is.EqualTo(submission.Question));
            Assert.That(result.Answer, Is.EqualTo(submission.Answer));
            Assert.That(result.Result, Is.EqualTo("incorrect (too high).  Please wait one minute before resubmitting."));
            Assert.That(result.DateTime, Is.Not.EqualTo(default(DateTime)));
            Assert.That(result.IdTime, Is.Not.Null.And.Not.Empty);

            // Optional output for debugging
            TestContext.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
        }

        [Test]
        public async Task Can_submit_answer_that_is_too_low_to_the_endpoint()
        {
            // Arrange
            var submission = new Submission
            {
                EmailAddress = "mycrazyemail@lmi.org",
                Question = "test",
                Answer = 777.775 // Make sure this is expected by the API
            };

            // Act
            Response result = await _submitter.SubmitAnswerAsync(submission);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.EmailAddress, Is.EqualTo(submission.EmailAddress));
            Assert.That(result.Question, Is.EqualTo(submission.Question));
            Assert.That(result.Answer, Is.EqualTo(submission.Answer));
            Assert.That(result.Result, Is.EqualTo("incorrect (too low).  Please wait one minute before resubmitting."));
            Assert.That(result.DateTime, Is.Not.EqualTo(default(DateTime)));
            Assert.That(result.IdTime, Is.Not.Null.And.Not.Empty);

            // Optional output for debugging
            TestContext.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
        }
    }
}
