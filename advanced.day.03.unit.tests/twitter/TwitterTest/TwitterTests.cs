using System;
using Xunit;
using Moq;
using _6.Twitter.Interfaces;

namespace TwitterTests
{
	public class TwitterTests
	{
		[Fact]
		public void ReceiveMessageShouldInvokeItsClientToWriteTheMessage()
		{
			var clientMock = new Mock<IClient>();

			clientMock.Verify(c => c.SendTweetToServer(It.IsAny<string>()), Times.Once);

			var testedClass = new _6.Twitter.Models.Tweet(clientMock.Object);
			testedClass.ReceiveMessage("test");


		}

		[Fact]
		public void ReceiveMessageShouldInvokeItsClientToSendTheMessageToTheServer()
		{
			var clientMock = new Mock<IClient>();

			clientMock.Verify(c => c.SendTweetToServer(It.IsAny<string>()), Times.Once);

			var testedClass = new _6.Twitter.Models.Tweet(clientMock.Object);
			testedClass.ReceiveMessage("test");

		}
	}
}
