using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMQWrapper
{
    class RouteAlreadyRegisteredException : Exception
    {
        public RouteAlreadyRegisteredException(Type type) : base("route already registered for type " + type)
        {

        }
    }
}
