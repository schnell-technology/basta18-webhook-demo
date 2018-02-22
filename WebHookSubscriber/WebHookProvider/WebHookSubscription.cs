using System;

namespace WebHookSubscriber.WebHookProvider
{
    public class WebHookSubscription
    {
        public Guid Id { get; set; }
        public string[] Messages { get; set; }
        public string Secret { get; set; }
        public string PublisherUrl { get; set; }

        public WebHookSubscription(string[] messages, string secret, string publisherUrl)
        {
            this.Id = Guid.NewGuid();
            this.Messages = messages;
            this.Secret = secret;
            this.PublisherUrl = publisherUrl;
        }
    }
}
