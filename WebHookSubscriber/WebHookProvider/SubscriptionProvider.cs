using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebHookSubscriber.WebHookProvider
{
    public class SubscriptionProvider
    {
        #region Singleton-Pattern
        private static SubscriptionProvider _current;
        public static SubscriptionProvider Current
        {
            get
            {
                if (_current == null)
                    _current = new SubscriptionProvider();

                return _current;
            }
        }
        #endregion

        #region Privates

        private List<WebHookSubscription> _subscriptions = new List<WebHookSubscription>();

        #endregion

        #region Constructor

        private SubscriptionProvider()
        {

        }

        #endregion


        #region Public Functions and Methods

        /// <summary>
        /// Create and store new subscription
        /// </summary>
        /// <param name="messages">Message-Types to subscribe</param>
        /// <param name="publisherUrl">URL of Publisher</param>
        /// <returns>Newly created WebHookSubscription-Object</returns>
        public async Task<WebHookSubscription> Subscribe(string[] messages, string publisherUrl)
        {
            var existing = _subscriptions.FirstOrDefault(r => String.Equals(publisherUrl, r.PublisherUrl, StringComparison.InvariantCultureIgnoreCase));
            if (existing != null)
                _subscriptions.Remove(existing);

            var newSubscription = new WebHookSubscription(messages, $"secret_{Guid.NewGuid().ToString("N").Substring(0, 5)}", publisherUrl);            

            //send request
            using (var client = new HttpClient())
            {
                Dictionary<string, string> form = new Dictionary<string, string>();
                form.Add("messages", String.Join(';', messages));
                form.Add("secret", newSubscription.Secret);
                form.Add("callbackUrl", $"http://localhost:9000/WebHook/{newSubscription.Id.ToString()}");

                var request = await client.PostAsync(newSubscription.PublisherUrl, new FormUrlEncodedContent(form));
                request.EnsureSuccessStatusCode();
            }

            _subscriptions.Add(newSubscription);
            return newSubscription;
        }

        /// <summary>
        /// Get Subscription by Id
        /// </summary>
        /// <param name="id">Id of Subscription</param>
        /// <returns>Requested Subscription</returns>
        public WebHookSubscription GetSubscription(Guid id)
        {
            return _subscriptions.FirstOrDefault(s => s.Id == id);
        }

        /// <summary>
        /// Check if a Subscription is available for specified WebHook-Message
        /// </summary>
        /// <param name="subscriptionId">Id of Subscription</param>
        /// <param name="message">Message-Type</param>
        /// <param name="secret">Secret</param>
        public void EnsureValidWebhookMessage(Guid subscriptionId, string message, string secret)
        {
            var subscription = GetSubscription(subscriptionId);
            var valid = subscription != null
                        && subscription.Messages.Any(m => String.Equals(m, message, StringComparison.InvariantCultureIgnoreCase))
                        && String.Equals(subscription.Secret, secret);

            if (!valid)
                throw new Exception("Message is not valid");
        }

        #endregion
    }
}
