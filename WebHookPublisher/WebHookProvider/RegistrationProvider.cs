using System;
using System.Collections.Generic;
using System.Linq;

namespace WebHookPublisher.WebHookProvider
{
    public class RegistrationProvider
    {
        #region Singleton-Pattern
        private static RegistrationProvider _current;
        public static RegistrationProvider Current { get
            {
                if (_current == null)
                    _current = new RegistrationProvider();

                return _current;
            } }
        #endregion

        #region Privates

        private List<WebHookRegistration> _register = new List<WebHookRegistration>();

        #endregion

        #region Constructor

        private RegistrationProvider()
        {

        }

        #endregion


        #region Public Functions and Methods

        /// <summary>
        /// Create and store a new WebHook-Registration
        /// </summary>
        /// <param name="messages">Message-Types</param>
        /// <param name="secret">Secret</param>
        /// <param name="callbackUrl">Callback-URL</param>
        /// <returns></returns>
        public WebHookRegistration Register(string[] messages, string secret, string callbackUrl)
        {
            var existing = _register.FirstOrDefault(r => String.Equals(callbackUrl, r.CallbackUrl, StringComparison.InvariantCultureIgnoreCase));
            if (existing != null)
                _register.Remove(existing);

            var newRegistration = new WebHookRegistration(messages, secret, callbackUrl);
            _register.Add(newRegistration);

            Console.WriteLine($"=> WebHook: Got new registration from {callbackUrl}");

            return newRegistration;
        }

        /// <summary>
        /// Get all Subscriber-Registrations for specified Message-Type
        /// </summary>
        /// <param name="message">Message-Type</param>
        /// <returns>Subscribed Registrations</returns>
        public IEnumerable<WebHookRegistration> GetRegistrationsForMessage(string message)
        {
            return _register.Where(r => r.Messages == null || !r.Messages.Any() || r.Messages.Any(m => String.Equals(m, message, StringComparison.InvariantCultureIgnoreCase)));
        }

        #endregion
    }    
}
