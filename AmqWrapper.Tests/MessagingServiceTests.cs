using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using AMQWrapper;
using FakeItEasy;

namespace AmqWrapper.Tests
{
    public class DummyMessage
    {
        [MessageProperty("MessageId")]
        public int Id { get; set; }
    }

    [TestFixture]
    public class MessagingServiceTests
    {
        private MessagingService _messagingService;
        private SendOptions _sendOptions;
        private IRouteFactory _routeFactory;
        private ISendRoute _sendRoute;

        [SetUp]
        public void SetUp()
        {
            _routeFactory = A.Fake<IRouteFactory>();
            _sendRoute = A.Fake<ISendRoute>();

            A.CallTo(() => _routeFactory.CreateSendRoute(A<Type>._, A<Route>._, A<SendOptions>._))
                .Returns(_sendRoute);

            _messagingService = new MessagingService(_routeFactory);
        }

        [Test]
        public void CanRegisterRouteForSending()
        {
            _messagingService.RegisterSend(typeof(DummyMessage), new Route("blahblahbla", "whoknows"), new SendOptions());            
        }

        [Test]
        public void WhenRegisteringARouteForSending_ShouldCreateANewSendRoute()
        {
            _messagingService.RegisterSend(typeof(DummyMessage), new Route("blahblahbla", "whoknows"), new SendOptions());

            A.CallTo(() => _routeFactory.CreateSendRoute(A<Type>._, A<Route>._, A<SendOptions>._)).MustHaveHappened();
        }

        [Test]
        public void WhenRegisteringARouteTwiceForTheSameType_ShouldThrowAnException()
        {
            _messagingService.RegisterSend(typeof(DummyMessage), new Route("blahblahbla", "whoknows"), new SendOptions());
            Assert.That(() => _messagingService.RegisterSend(typeof(DummyMessage), new Route("blahblahbla", "whoknows"), new SendOptions()), Throws.InstanceOf<RouteAlreadyRegisteredException>());           
        }

        [Test]
        public void WhenSendingAMessageWithAnUnregistered_ShouldThrowException()
        {
            Assert.That(() => _messagingService.Send(new DummyMessage()), Throws.InstanceOf<RouteNotRegisteredException>());
        }

        [Test]
        public void WhenSendingANullMessage_ShouldThrowException()
        {
            Assert.That(() => _messagingService.Send(null), Throws.InstanceOf<ArgumentNullException>());
        }

        [Test]
        public void WhenSendingMessageOfARegisteredType_ShouldSendMessageToSendRoute()
        {
            _messagingService.RegisterSend(typeof(DummyMessage), new Route("blahblahbla", "whoknows"), new SendOptions());

            var msg = new DummyMessage();
            _messagingService.Send(msg);

            A.CallTo(() => _sendRoute.Send(msg, null)).MustHaveHappened();
        }
        

    }
}
