using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Amazon.SQS;
using Amazon.SQS.Model;

namespace dotnetSQSPublisher.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SendMessageSQSController : ControllerBase
    {
        AmazonSQSClient sqsClient = new AmazonSQSClient();
        private string? queueUrl = Environment.GetEnvironmentVariable("UrlQueue");

        private readonly ILogger<SendMessageSQSController> _logger;

        public SendMessageSQSController(ILogger<SendMessageSQSController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<object> Post()
        {
            // Sending a message
            var sendMessageRequest = new SendMessageRequest
            {
                QueueUrl = queueUrl,
                MessageBody = "Hello from C#!"
            };
            await sqsClient.SendMessageAsync(sendMessageRequest);
            return new { message ="enviado con sucesso" };
        }
    }
}
