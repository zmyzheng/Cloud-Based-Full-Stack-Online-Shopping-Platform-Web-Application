namespace EmailService.Test
{
    using System;
    using System.Collections.Generic;
    using Amazon.Lambda.SNSEvents;
    using Amazon.Lambda.TestUtilities;
    using EmailService;
    using EmailService.Interface;
    using Moq;
    using Newtonsoft.Json;
    using Shared.Email;
    using Xunit;

    public class FunctionTest
    {
        // [Fact]
        // public void EmailShouldSend()
        // {
        //     var mockEmailService = new Mock<IEmailService>();
        //     mockEmailService.Setup(e => e.SendAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
        //                     .Callback(() => Console.WriteLine("Email send."));

        //     var function = new Function(mockEmailService.Object);
        //     var context = new TestLambdaContext();

        //     var emailReq = new EmailRequest()
        //     {
        //         From = "From",
        //         To = "To",
        //         Subject = "Test",
        //         Body = "Test body"
        //     };
        //     var message = JsonConvert.SerializeObject(emailReq);

        //     var snsEvent = new SNSEvent()
        //     {
        //         Records = new List<SNSEvent.SNSRecord>()
        //         {
        //             new SNSEvent.SNSRecord()
        //             {
        //                 Sns = new SNSEvent.SNSMessage()
        //                 {
        //                     Message = message
        //                 }
        //             }
        //         }
        //     };

        //     function.FunctionHandler(snsEvent, context);
        // }
    }
}
