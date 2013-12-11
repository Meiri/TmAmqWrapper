using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMQWrapper
{
    interface IRouteFactory
    {
        ISendRoute CreateSendRoute(Type objectType, Route route, SendOptions options);
    }
}
