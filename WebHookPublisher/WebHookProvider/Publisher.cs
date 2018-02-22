using System;
using System.Linq;
using System.Net.Http;

namespace WebHookPublisher.WebHookProvider
{
    public class Publisher
    {
        #region Singleton
        private static Publisher _current;
        public static Publisher Current { get
            {
                if (_current == null)
                    _current = new Publisher();
                return _current;
            }
        }
        #endregion

        #region Constructor

        private Publisher()
        {

        }

        #endregion

        #region Public Functions and Methods

        /// <summary>
        /// Send WebHook-Message to Subscriber
        /// </summary>
        /// <typeparam name="T">Content-Type of Message</typeparam>
        /// <param name="message">Message-Type</param>
        /// <param name="content">Content of Message</param>
        public void PublishMessage<T>(string message, T content)
        {
            var registrar = RegistrationProvider.Current.GetRegistrationsForMessage(message);
            registrar.ToList().ForEach(async (register) =>
            {
                try
                {
                    using (var client = new HttpClient())
                    {
                        var httpContent = GetContent<T>(register, content);
                        httpContent.Headers.Add("X-WEBHOOK-MESSAGE", message);
                        httpContent.Headers.Add("X-WEBHOOK-SECRET", register.Secret);

                        var response = await client.PostAsync(register.CallbackUrl, httpContent);
                        response.EnsureSuccessStatusCode();
                        Console.WriteLine($"=> WebHook: Sent message to {register.CallbackUrl}");
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Error: Could not send WebHook-Message");
                }
            });
        }

        #endregion

        #region Private Functions and Methods

        /// <summary>
        /// Get HttpContent by Registration and Message-Content
        /// </summary>
        /// <typeparam name="T">Type of Message-Content</typeparam>
        /// <param name="register">WebHook-Registration</param>
        /// <param name="content">WebHook-Content</param>
        /// <returns></returns>
        private HttpContent GetContent<T>(WebHookRegistration register, T content)
        {
            //get specified content?
            //e.g. XML, CSV, proprietary format?

            //default: json
            return new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(content), System.Text.Encoding.UTF8, "application/json");            
        }

        #endregion
    }
}
