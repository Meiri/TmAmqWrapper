using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMQWrapper
{
    public class Route
    {
        public Route(string brokerName, string destination)
        {
            BrokerName = brokerName;
            Destination = destination;
        }

        ///  <add key="Mq.Broker.{BrokerName}" value="failover:(tcp://....)" />
        public string BrokerName
        {
            get ; private set;
        }

        /// "topic:TestTopic" or "queue:TestQueue"
        public string Destination
        {
            get;
            private set;
        }
    }
}
