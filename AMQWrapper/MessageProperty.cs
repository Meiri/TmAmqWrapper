using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMQWrapper
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]  // nice to have
    public class MessagePropertyAttribute : Attribute
    {
        public MessagePropertyAttribute()
        {
        }

        public MessagePropertyAttribute(string propertyName)
        {
            Name = propertyName;
        }

        public string Name { get; set; }
    }
}
