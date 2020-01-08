using System;
using Xunit;
using Moq;
using _6.Twitter.Interfaces;

namespace TwitterTest
{
	public class MicrowaveOvenTests
	{
		[Fact]
		public void SendTweetToServerShouldSendTheMessageToItsServer()
		{
			var writerMock = new Mock<IWriter>();

			var repomock = new Mock<ITweetRepository>();

			repomock.Verify(r => r.SaveTweet(It.IsAny<string>()), Times.Once);

			var testedClass = new _6.Twitter.Models.MicrowaveOven(writerMock.Object, repomock.Object);

			testedClass.SendTweetToServer("test");

			


		}

		[Fact]
		public void WriteTweetShouldCallItsWriterWithTheTweetsMessage()
		{
			var writerMock = new Mock<IWriter>();

			var repomock = new Mock<ITweetRepository>();

			writerMock.Verify(r => r.WriteLine(It.IsAny<string>()), Times.Once);

			var testedClass = new _6.Twitter.Models.MicrowaveOven(writerMock.Object, repomock.Object);

			testedClass.WriteTweet("test");

		}
	}
}
