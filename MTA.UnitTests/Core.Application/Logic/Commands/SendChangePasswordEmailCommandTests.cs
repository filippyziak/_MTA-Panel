﻿using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Moq;
using MTA.Core.Application.Logic.Handlers.Commands;
using MTA.Core.Application.Logic.Requests.Commands;
using MTA.Core.Application.Logic.Responses.Commands;
using MTA.Core.Application.Models;
using MTA.Core.Application.Results;
using MTA.Core.Application.Services;
using NUnit.Framework;

namespace MTA.UnitTests.Core.Application.Logic.Commands
{
    [TestFixture]
    public class SendChangePasswordEmailCommandTests
    {
        private SendChangePasswordEmailRequest request;
        private Mock<IUserTokenGenerator> userTokenGenerator;
        private Mock<ICryptoService> cryptoService;
        private Mock<IEmailSender> emailSender;
        private Mock<IEmailTemplateGenerator> emailTemplateGenerator;
        private SendChangePasswordEmailCommand sendChangePasswordEmailCommand;

        private const string Test = "test";

        [SetUp]
        public void SetUp()
        {
            var generateChangePasswordTokenResult = new GenerateChangePasswordTokenResult
            {
                Email = Test,
                Token = Test,
                Username = Test
            };

            var emailTemplate = new EmailTemplate
            {
                TemplateName = Test,
                Subject = Test,
                Content = Test,
                AllowedParameters = new[] {"{{username}}", "{{callbackUrl}}"}
            };

            request = new SendChangePasswordEmailRequest
            {
                OldPassword = Test,
                NewPassword = Test
            };

            userTokenGenerator = new Mock<IUserTokenGenerator>();
            cryptoService = new Mock<ICryptoService>();
            emailSender = new Mock<IEmailSender>();
            emailTemplateGenerator = new Mock<IEmailTemplateGenerator>();
            var configuration = new Mock<IConfiguration>();

            userTokenGenerator.Setup(c => c.GenerateChangePasswordToken(It.IsAny<string>()))
                .ReturnsAsync(generateChangePasswordTokenResult);
            cryptoService.Setup(c => c.Encrypt(It.IsAny<string>()))
                .Returns(Test);
            configuration.Setup(c => c.GetSection(It.IsAny<string>()))
                .Returns(new Mock<IConfigurationSection>().Object);
            emailTemplateGenerator.Setup(etg => etg.FindEmailTemplate(It.IsAny<string>()))
                .ReturnsAsync(emailTemplate);
            emailSender.Setup(es => es.Send(It.IsAny<EmailMessage>()))
                .ReturnsAsync(true);

            sendChangePasswordEmailCommand = new SendChangePasswordEmailCommand(userTokenGenerator.Object,
                cryptoService.Object, emailSender.Object, emailTemplateGenerator.Object, configuration.Object);
        }

        [Test]
        public async Task Handle_WhenCalled_SendChangePasswordEmailResponse()
        {
            var result = await sendChangePasswordEmailCommand.Handle(request, It.IsAny<CancellationToken>());

            Assert.That(result, Is.TypeOf<SendChangePasswordEmailResponse>());
            Assert.That(result.IsSucceeded, Is.True);
        }
    }
}