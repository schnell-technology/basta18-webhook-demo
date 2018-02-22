using System;
using Microsoft.AspNetCore.Mvc;
using WebHookPublisher.Messages;

namespace WebHookPublisher.Controllers
{
    [Produces("application/json")]
    [Route("api/Orders")]
    public class OrdersController : Controller
    {
        /// <summary>
        /// Create a new order and publish a WebHook-Message
        /// </summary>
        /// <param name="customerName">Customer-Name</param>
        /// <param name="customerAddress">Customer-Address</param>
        /// <param name="product">Ordered product</param>
        /// <param name="amount">Amount of ordered product</param>
        [HttpPost]
        public void Post([FromForm]string customerName, [FromForm]string customerAddress, [FromForm]string product, [FromForm]int amount)
        {
            //validate and store order
            //...
            //...

            //create webhook-message
            var msgContent = new OrderMessage();
            msgContent.OrderId = Guid.NewGuid();
            msgContent.Timestamp = DateTime.Now;

            msgContent.Customer.Name = customerName;
            msgContent.Customer.Address = customerAddress;

            msgContent.Positions.Add(new OrderPosition() { Product = product, Amount = amount });

            //send webhook-message
            WebHookProvider.Publisher.Current.PublishMessage<OrderMessage>("Bestellung_Neu", msgContent);
        }
        
    }
}
