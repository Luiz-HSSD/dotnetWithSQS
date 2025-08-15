using Amazon.SQS;
using Amazon.SQS.Model;

AmazonSQSClient sqsClient = new AmazonSQSClient();
string? queueUrl = Environment.GetEnvironmentVariable("UrlQueue");
// See https://aka.ms/new-console-template for more information
while (true)
{
    // Receiving messages
    var receiveMessageRequest = new ReceiveMessageRequest
    {
        QueueUrl = queueUrl,
        MaxNumberOfMessages = 1
    };
    var receiveMessageResponse = await sqsClient.ReceiveMessageAsync(receiveMessageRequest);
    if (receiveMessageResponse.Messages != null)
    {
        foreach (var message in receiveMessageResponse.Messages)
        {
            Console.WriteLine($"Received message: {message.Body}");

            // Deleting the message after processing
            var deleteMessageRequest = new DeleteMessageRequest
            {
                QueueUrl = queueUrl,
                ReceiptHandle = message.ReceiptHandle
            };
            await sqsClient.DeleteMessageAsync(deleteMessageRequest);
            Console.WriteLine("Message deleted.");
        }
    }
    Thread.Sleep(500);
}

