using Cmc.Core.PaymentProvider.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmc.Core.PaymentProvider.AciPaymentBridge
{
    public interface IAciPaymentBridgeProcessor
    {
        /// <summary>
        /// Redirects to hosted page URL.
        /// </summary>
        /// <param name="payLoad">The pay load.</param>
        /// <returns></returns>
        GetOrRedirectToProviderUrlResponse ConstructHostedPageUrl(GetOrRedirectToProviderUrlRequest payLoad);

        /// <summary>
        /// to build and post the form.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postData"></param>
        /// <returns></returns>
        string PostFormToUrl(string url, Dictionary<string, object> postData);
    }
}
