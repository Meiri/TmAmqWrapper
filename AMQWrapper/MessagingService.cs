using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;

namespace AMQWrapper
{




    public interface IListener<TMessage>
    {
        void Handle(TMessage message);
    }

    public interface IListenerWithProperties<TMessage>
    {
        void Handle(TMessage message, IDictionary<string,object> listener);
    }

    public interface IMessageBus
    {
        void Send(object msg);
        void Send(object msg, IDictionary<string, object> properties);

        IDisposable Listen<TMessage>(Action<TMessage> listener, string selector = null);
        IDisposable Listen<TMessage>(Action<TMessage, IDictionary<string,object>> listener, string selector = null);
        IDisposable Listen<TMessage>(IListener<TMessage> listener, string selector = null);
        IDisposable Listen<TMessage>(IListenerWithProperties<TMessage> listener, string selector = null);
    }



    public class SendOptions
    {
    }

    public class ReceiveOptions
    {
    }

    public interface IMessagingService : IMessageBus
    {
        IDisposable RegisterSend(Type objectType, Route route, SendOptions options);
        IDisposable RegisterReceive(Type objectType, Route route, ReceiveOptions options);
    }

    internal class MessagingService : IMessagingService
    {
        private ConcurrentDictionary<Type, ISendRoute> _outgoingRoutes = new ConcurrentDictionary<Type, ISendRoute>();
        private IRouteFactory _routeFactory;

        public MessagingService(IRouteFactory _routeFactory)
        {
            this._routeFactory = _routeFactory;
        }

        public IDisposable  RegisterSend(Type objectType, Route route, SendOptions options)
        {
            ISendRoute sendRoute = _routeFactory.CreateSendRoute(objectType, route, options);

            if (!_outgoingRoutes.TryAdd(objectType, sendRoute))
            {
                throw new RouteAlreadyRegisteredException(objectType);
            };

            return null;
        }

        public IDisposable  RegisterReceive(Type objectType, Route route, ReceiveOptions options)
        {
            return null;
        }

        public void  Send(object msg)
        {
 	        Send(msg, null);
        }

        public void  Send(object msg, IDictionary<string,object> properties)
        {
            if(msg == null) {
                throw new ArgumentNullException("msg");
            }

 	        ISendRoute sendRoute;
            
            if(!_outgoingRoutes.TryGetValue(msg.GetType(), out sendRoute)) {
                throw new RouteNotRegisteredException(msg.GetType());
            };
            
            sendRoute.Send(msg, properties);
        }

        public IDisposable  Listen<TMessage>(Action<TMessage> listener, string selector = null)
        {
 	        throw new NotImplementedException();
        }

        public IDisposable  Listen<TMessage>(Action<TMessage, IDictionary<string,object>> listener, string selector = null)
        {
 	        throw new NotImplementedException();
        }

        public IDisposable  Listen<TMessage>(IListener<TMessage> listener, string selector = null)
        {
 	        throw new NotImplementedException();
        }

        public IDisposable  Listen<TMessage>(IListenerWithProperties<TMessage> listener, string selector = null)
        {
 	        throw new NotImplementedException();
        }
    }
}
