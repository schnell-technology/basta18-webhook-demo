using System;
using Microsoft.AspNetCore.Mvc;
using WebHookPublisher.WebHookProvider;

namespace WebHookPublisher.Controllers
{
    [Produces("application/json")]
    [Route("WebHook")]
    public class WebHookController : Controller
    {
        /// <summary>
        /// Handle a subscription request and store registration
        /// </summary>
        /// <param name="messages">Message-Types (joined String-Array)</param>
        /// <param name="secret">Secret of subscription</param>
        /// <param name="callbackUrl">Callback-URL</param>
        [HttpPost()]
        public void HandleSubscriptionRequest([FromForm]string messages, [FromForm]string secret, [FromForm]string callbackUrl)
        {
            //split message-types
            string[] messagesArray = new string[0];
            if (!String.IsNullOrEmpty(messages))
                messagesArray = messages.Split(';');

            //register a new subscription
            RegistrationProvider.Current.Register(messagesArray, secret, callbackUrl);
        }
        
    }
}
