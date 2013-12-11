using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMQWrapper
{
    interface ISendRoute
    {
        void Send(object message, IDictionary<string, object> properties);
    }

    class SendRoute : ISendRoute
    {


        public void Send(object message, IDictionary<string,object> properties)
        {
            // 1. convert the .Net object to xml/protobuf
            // 2. create an IMessage to transport the data
            // 3. apply any properties to the message

            IMessageBus message = A.Fake<>();
            // 4. send the message

            throw new NotImplementedException();
        }
    }
}
