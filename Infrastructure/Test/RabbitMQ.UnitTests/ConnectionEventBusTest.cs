using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using RabbitMQ.Client;
using RabbitMQ.Settings;

namespace RabbitMQ.UnitTests
{
    public class ConnectionEventBusTest
    {
        [TestFixture]
        public class ConnectionEventBusTests
        {
            private readonly Mock<ConnectionFactory> _moqConnectionFactory;
            private readonly Mock<IOptions<EventQueueSettings>> _settings;
            
            public ConnectionEventBusTests()
            {
                _moqConnectionFactory = new Mock<ConnectionFactory>();
                _settings = new Mock<IOptions<EventQueueSettings>>();
            }
            
            [Test]
            public void CreateSession_ShouldReturnIModel()
            {
               var test = new Mock<ConnectionFactory>().Setup(m => m.CreateConnection());
             


            }

            [Test]
            public void TryConnect_IsConnect_ShouldReturnTrue()
            {
                var test = new Mock<IConnection>().Setup(p=> p.IsOpen).Returns(true);
                
              //  var object = new ConnectionEventBus(_moqConnectionFactory, _settings);
            }
        }
    }
}