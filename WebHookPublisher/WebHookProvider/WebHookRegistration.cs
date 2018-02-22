using System;

namespace WebHookPublisher.WebHookProvider
{
    public class WebHookRegistration
    {
        public string[] Messages { get; set; }
        public string Secret { get; set; }
        public string CallbackUrl { get; set; }

        public WebHookRegistration(string[] messages, string secret, string callbackUrl)
        {
            Messages = messages;
            Secret = secret;
            CallbackUrl = callbackUrl;
        }
    }
}
