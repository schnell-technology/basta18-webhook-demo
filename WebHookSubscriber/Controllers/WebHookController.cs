using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebHookSubscriber.WebHookProvider;

namespace WebHookSubscriber.Controllers
{
    [Produces("application/json")]
    [Route("WebHook")]
    public class WebHookController : Controller
    {
        /// <summary>
        /// Creates a new WebHook-Subscription
        /// </summary>
        [HttpPost("CreateSubscription")]
        public async Task<WebHookSubscription> CreateSubscription([FromForm]string[] messages, [FromForm]string webhookPublisherUrl)
        {
            var subscription = await SubscriptionProvider.Current.Subscribe(messages, webhookPublisherUrl);
            return subscription;
        }

        /// <summary>
        /// Handles a WebHook-Message from a subscribed WebHookPublisher
        /// </summary>
        /// <param name="id">Id of WebHookSubscription</param>
        /// <param name="content">Content of Message</param>
        /// <param name="message">Message-Type from Header</param>
        /// <param name="secret">Secret of WebHookSubscription</param>
        [HttpPost("{id}")]
        public void HandleMessage(
            [FromRoute]Guid id, 
            [FromBody]dynamic content,
            [FromHeader(Name ="X-WEBHOOK-MESSAGE")]string message, 
            [FromHeader(Name = "X-WEBHOOK-SECRET")]string secret)
        {
            SubscriptionProvider.Current.EnsureValidWebhookMessage(id, message, secret);
            Console.Write($"WebHook-Message: {message}\n===BEGIN===\n{content.ToString()}\n===END===");
        }

    }
}
