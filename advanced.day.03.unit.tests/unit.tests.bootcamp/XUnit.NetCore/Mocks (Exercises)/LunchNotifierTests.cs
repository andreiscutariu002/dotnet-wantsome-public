namespace xUnit.NetCore.Mocks
{
    using Moq;
    using ProductionCode.MockingExample;
    using Xunit;
    using System.Collections.Generic;

    public class LunchNotifierTests
    {
        [Theory]
        [InlineData("2017-01-01 13:00:00", LunchNotifier.LateLunchTemplate)]
        [InlineData("2017-01-01 12:59:59", LunchNotifier.RegularLunchTemplate)]
        public void Test_CorrectTemplateIsUsed_LateLunch_Seam(string currentTime, string expectedTemplate)
        {
            //
            // Create mocks:
            //
            var loggerMock = new Mock<ILogger>();

            var bobMock = new Mock<IEmployee>();
            /*
            * Configure mock so that employee is considered working today and gets notifications via email
            *
            */
            bobMock.Setup(b => b.IsWorkingOnDate(It.IsAny<System.DateTime>())).Returns(true);
            bobMock.Setup(b => b.GetNotificationPreference()).Returns(LunchNotifier.NotificationType.Email);

            var employeeServiceMock = new Mock<IEmployeeService>();
            /*
            * Configure mock so to return employee from above
            *
            */
            var list = new List<IEmployee>();
            list.Add(bobMock.Object);

            employeeServiceMock.Setup(e => e.GetEmployeesInNewYorkOffice()).Returns(list);

            var notificationServiceMock = new Mock<INotificationService>();


            //
            // Create instance of class I'm testing:
            //
            var classUnderTest = new Moq.Mock<LunchNotifier_UsingSeam>(notificationServiceMock.Object, employeeServiceMock.Object, loggerMock.Object)
            { CallBase = true };

            classUnderTest.Setup(x => x.GetDateTime())
                .Returns(System.DateTime.Parse(currentTime));
            /*
             * Create a partial mock of the LunchNotifier_UsingSeam class and change the GetDateTime() behavior to return DateTime.Parse(currentTime)
             *
             */

            //
            // Run some logic to test:
            //
            classUnderTest.Object.SendLunchtimeNotifications();

            //
            // Check the results:
            //
            notificationServiceMock.Verify(x => x.SendEmail(bobMock.Object, expectedTemplate), Times.Once);
        }

        [Fact]
        public void Test_EmployeeInOfficeGetsNotified()
        {
            //
            // Create mocks:
            //
            var loggerMock = new Mock<ILogger>();

            var bobMock = new Mock<IEmployee>();
            /*
             * Configure mock so that employee is considered working today and gets notifications via email
             *
             */

            bobMock.Setup(foo => foo.IsWorkingOnDate(It.IsAny<System.DateTime>())).Returns(true);
            bobMock.Setup(b => b.GetNotificationPreference()).Returns(LunchNotifier.NotificationType.Email);


            var employeeServiceMock = new Mock<IEmployeeService>();
            /*
             * Configure mock so to return employee from above
             *
             */
             employeeServiceMock.Setup(e=>e.GetEmployeesInNewYorkOffice()).Returns(new IEmployee[1] { bobMock.Object});

            var notificationServiceMock = new Mock<INotificationService>();
            /*
            * Configure mock so that you can verify a notification was sent via email
            *
            */

            //
            // Create instance of class I'm testing:
            //
            var classUnderTest = new LunchNotifier(notificationServiceMock.Object, employeeServiceMock.Object,
                loggerMock.Object);

            //
            // Run some logic to test:
            //
            classUnderTest.SendLunchtimeNotifications();

            //
            // Check the results:
            //
            notificationServiceMock.Verify(x => x.SendEmail(It.IsAny<IEmployee>(), It.IsAny<string>()), Times.Once);
            /*
            * Add verifications to prove emails notification was sent
            *
            */
        }


        [Fact]
        public void Test_ExceptionDoesNotStopNotifications()
        {
            //
            // Create mocks:
            //
            var loggerMock = new Mock<ILogger>();
            /*
            * Configure mock so that you can verify a error was logged
            *
            */
            

            var bobMock = new Mock<IEmployee>();
            /*
             * Configure mock so that employee is considered working today and gets notifications via email
             *
             */
            bobMock.Setup(b => b.IsWorkingOnDate(It.IsAny<System.DateTime>())).Returns(true);
            bobMock.Setup(b => b.GetNotificationPreference()).Returns(LunchNotifier.NotificationType.Email);

            var marthaMock = new Mock<IEmployee>();
            marthaMock.Setup(b => b.IsWorkingOnDate(It.IsAny<System.DateTime>())).Returns(true);
            marthaMock.Setup(b => b.GetNotificationPreference()).Returns(LunchNotifier.NotificationType.Email);

            /*
             * Configure mock so that employee is considered working today and gets notifications via email
             *
             */


            var employeeServiceMock = new Mock<IEmployeeService>();
            employeeServiceMock.Setup(l => l.GetEmployeesInNewYorkOffice()).Returns(new IEmployee[2] { bobMock.Object, marthaMock.Object });

            /*
             * Configure mock so to return both employees from above
             *
             */


            var notificationServiceMock = new Mock<INotificationService>();
            /*
             * Configure mock to throw an exception when attempting to send notification via email
             *
             */
            notificationServiceMock.Setup(n => n.SendEmail(It.IsAny<IEmployee>(), It.IsAny<string>())).Throws(new System.Exception("testException"));


            //
            // Create instance of class I'm testing:
            //
            var classUnderTest = new LunchNotifier(notificationServiceMock.Object, employeeServiceMock.Object,
                loggerMock.Object);

            //
            // Run some logic to test:
            //
            classUnderTest.SendLunchtimeNotifications();

            //
            // Check the results:
            //

            /*
             * Add verifications to prove emails notification were attempted twice
             *
             * Add verification that error logger was called
             *
             */
            notificationServiceMock.Verify(x => x.SendEmail(It.IsAny<IEmployee>(), It.IsAny<string>()), Times.Exactly(2));
            loggerMock.Verify(x => x.Error(It.IsAny<System.Exception>()), Times.Exactly(2));
        }
    }
}
