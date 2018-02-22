using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace WebHookPublisher.Messages
{
    [DataContract]
    public class OrderMessage
    {
        [DataMember]
        public Guid OrderId { get; set; }

        [DataMember]
        public DateTime Timestamp { get; set; }

        [DataMember]
        public OrderCustomer Customer { get; set; }

        [DataMember]
        public List<OrderPosition> Positions { get; set; }

        public OrderMessage()
        {
            this.Positions = new List<OrderPosition>();
            this.Customer = new OrderCustomer();
        }
    }

    [DataContract]
    public class OrderCustomer
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Address { get; set; }
    }

    [DataContract]
    public class OrderPosition
    {
        [DataMember]
        public string Product { get; set; }

        [DataMember]
        public int Amount { get; set; }
    }
}
