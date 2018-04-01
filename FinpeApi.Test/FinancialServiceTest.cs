using FinpeApi.Services;
using Moq;
using Xunit;

namespace FinpeApi.Test
{
    public class FinancialServiceTest
    {
        [Fact]
        public void AddStatement_ShouldNotify()
        {
            // Arrange
            var notificationServiceMock = new Mock<INotificationService>();

            // Act

            // Assert

        }
    }
}
