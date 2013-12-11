using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMQWrapper
{
    class RouteNotRegisteredException : Exception
    {
        public RouteNotRegisteredException(Type t) : base("no route is registered for type " + t)
        {            
        }
    }
}
