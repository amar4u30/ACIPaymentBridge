using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using Cmc.Core.ComponentModel;
using Cmc.Core.Diagnostics;
using Cmc.Core.PaymentProvider.Interfaces;
using Cmc.Core.PaymentProvider.Messages;

namespace Cmc.Core.PaymentProvider.AciPaymentBridge
{
    /// <summary>
    /// Class responsible for initiating the Aci Payment Bridge call.
    /// </summary>
    /// <seealso cref="Cmc.Core.PaymentProvider.Interfaces.IPaymentAdaptor" />
    [Injectable(InstanceScope.Singleton)]
    public class AciPaymentBridgeAdaptor : IPaymentAdaptor
    {
        /// <summary>
        /// The ACI Payment Bridge processor
        /// </summary>
        private readonly IAciPaymentBridgeProcessor _aciPaymentBridgeProcessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="AciPaymentBridgeAdaptor"/> class.
        /// </summary>
        /// <param name="aciPaymentBridgeProcessor"></param>
        public AciPaymentBridgeAdaptor(IAciPaymentBridgeProcessor aciPaymentBridgeProcessor)
        {
            _aciPaymentBridgeProcessor = aciPaymentBridgeProcessor;
        }

        /// <summary>
        /// get or redirect to host page url for ACI Payment Bridge.
        /// </summary>
        /// <param name="payLoad"></param>
        /// <returns></returns>
        public GetOrRedirectToProviderUrlResponse GetOrRedirectToProviderUrl(GetOrRedirectToProviderUrlRequest payLoad)
        {
            //_logger.Info("Reached GetOrRedirectToProviderUrl");
            var paymentResponse = _aciPaymentBridgeProcessor.ConstructHostedPageUrl(payLoad);

            if (paymentResponse.ConstructedUrl != null)
            {
                if (payLoad.PaymentRequest.IsRedirectEnabled != null && (bool)payLoad.PaymentRequest.IsRedirectEnabled)
                    HttpContext.Current.Response.Write(paymentResponse.ConstructedUrl);
            }
            return paymentResponse;
        }

        /// <summary>
        /// verifies the payment
        /// </summary>
        /// <param name="profilePayLoad"></param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public BatchPaymentProfileResponse ProcessBatchPaymentsByProfile(BatchPaymentProfileRequest profilePayLoad)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///  Processes the payment by profile.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public VerifyPaymentResponse VerifyPayment(VerifyPaymentRequest request)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Processes the batch payment by profile.
        /// </summary>
        /// <param name="profilePayLoad"></param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public PaymentProfileResponse ProcessPaymentByProfile(PaymentProfileRequest profilePayLoad)
        {
            throw new NotImplementedException();
        }
    }
}
