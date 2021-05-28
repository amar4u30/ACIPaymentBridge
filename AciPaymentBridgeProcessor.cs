using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Cmc.Core.ComponentModel;
using Cmc.Core.Configuration;
using Cmc.Core.PaymentProvider.Messages;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Serialization;
using System.IO;
using Cmc.Core.Diagnostics;
using System.Text;
using Newtonsoft.Json;

namespace Cmc.Core.PaymentProvider.AciPaymentBridge
{
    /// <summary>
    /// This Class provides API method calls to Aci
    /// </summary>
    /// <seealso cref="Cmc.Core.PaymentProvider.Aci.IAciProcessor" />
    [Injectable(InstanceScope.Singleton)]
    internal class AciPaymentBridgeProcessor : IAciPaymentBridgeProcessor
    {
        
        /// <summary>
        /// The Editamount Flag.
        /// </summary>
        private readonly bool _editAmount;  
        
        /// <summary>
        /// The Lock amount flag.
        /// </summary>
        private readonly bool _lockAmount;   
        
        /// <summary>
        /// The Payment method.
        /// </summary>
        private readonly string _payBy;  
        
        /// <summary>
        /// The Product
        /// </summary>
        private readonly string _paymentmethoddisplay;

        /// <summary>
        /// Logger
        /// </summary>
        private readonly ILogger _logger;


        /// <summary>
        /// Initializes a new instance of the <see cref="AciPaymentBridgeProcessor"/> class.
        /// </summary>
        public AciPaymentBridgeProcessor(IConfigurationManager configurationManager, ILogger logger)
        {
            _logger = logger;
            _logger.Debug("AciPaymentBridgeProcessor");
            _editAmount = false;
            _lockAmount = true;
            _payBy = "CC";
            _paymentmethoddisplay = "01";
            //_ecommerceIndicator = configurationManager.GetAppSetting("Aci:EcommerceIndicator", "ECOMMERCE");
            //_product = configurationManager.GetAppSetting("Aci:Product", "CONNECT_WEB");

            //_configReqPaymentDate = configurationManager.GetAppSetting("Aci:RequestedPaymentDate", string.Empty);

            //_postHttpclient = new HttpClient();

            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 |
            //                                       SecurityProtocolType.Tls12;
            //_postHttpclient.Timeout = TimeSpan.FromMilliseconds(120000);

            //_suffix = "Mr";

            //InitializeMapper();
        }
        /// <summary>
        /// Redirects to hosted page URL.
        /// </summary>
        /// <param name="payLoad">The pay load.</param>
        /// <returns></returns>
        public GetOrRedirectToProviderUrlResponse ConstructHostedPageUrl(
            GetOrRedirectToProviderUrlRequest payLoad)
        {
            using (new LogScope(_logger))
            {
                var returnUrl = payLoad.PaymentRequest.ReturnUrl + $"?User1={payLoad.PaymentRequest.UserDefinedField1}&User2={payLoad.PaymentRequest.UserDefinedField2}&User3={payLoad.PaymentRequest.UserDefinedField3}&User10={payLoad.PaymentRequest.CorrelationId}";
                var cancelUrl = payLoad.PaymentRequest.CancelUrl + $"?status=failure&User1={payLoad.PaymentRequest.UserDefinedField1}&User2={payLoad.PaymentRequest.UserDefinedField2}&User3={payLoad.PaymentRequest.UserDefinedField3}&User10={payLoad.PaymentRequest.CorrelationId}";
                var ErroUrl = payLoad.PaymentRequest.ErrorUrl + $"?status=failure&User1={payLoad.PaymentRequest.UserDefinedField1}&User2={payLoad.PaymentRequest.UserDefinedField2}&User3={payLoad.PaymentRequest.UserDefinedField3}&User10={payLoad.PaymentRequest.CorrelationId}";
                var req = new PaymentRequstDto
                {
                    ProductId = payLoad.PaymentRequest.ProviderInfo.MerchantCode,
                    PaymentAmount = payLoad.PayeePaymentDetails.Amount,
                    //Suffix=_suffix,
                    FirstName = payLoad.PayeePaymentDetails.FirstName,
                    MiddleName = payLoad.PayeePaymentDetails.MiddleName,
                    LastName = payLoad.PayeePaymentDetails.LastName,
                    Address1 = payLoad.PayeePaymentDetails.Address,
                    CityName = payLoad.PayeePaymentDetails.City,
                    //ProvinceCd = "GA"/*payLoad.PayeePaymentDetails.State*/,
                    PostalCd = payLoad.PayeePaymentDetails.Zip,
                    PhoneNum = payLoad.PayeePaymentDetails.Phone,
                    Email = payLoad.PayeePaymentDetails.Email,
                    ReturnUrl = returnUrl.ToString(),
                    ErrorUrl = ErroUrl.ToString(),
                    CancelUrl = cancelUrl.ToString(),
                    EditAmount = _editAmount,
                    LockAmount = _lockAmount,
                    PayBy = _payBy,
                    Paymentmethoddisplay = _paymentmethoddisplay
                };
                GetOrRedirectToProviderUrlResponse paymentResponse = new GetOrRedirectToProviderUrlResponse();
                var postData = JsonConvert.DeserializeObject<Dictionary<string, object>>(JsonConvert.SerializeObject(req));
                postData.Add("cde-paymentIdentifier-0", payLoad.PaymentRequest.CorrelationId);
                var dataToACI = new Dictionary<string, object>();
                foreach (var item in postData)
                {
                    if (item.Value != null && !string.IsNullOrEmpty(item.Value.ToString()))
                        dataToACI.Add(item.Key,item.Value);
                }

                paymentResponse.ConstructedUrl = PostFormToUrl(payLoad.PaymentRequest.ProviderInfo.HostedPageUrl, dataToACI);

                _logger.Trace("Returning Payment Response for ConstuctHostedPageUrl");
                return paymentResponse;
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// to build and post the form.
        /// </summary>
        /// <param name="url">posted url</param>
        /// <param name="postData">post data</param>
        /// <returns></returns>
        public string PostFormToUrl(string url, Dictionary<string, object> postData)
        {
            string formId = "__PostForm";

            StringBuilder strForm = new StringBuilder();
            strForm.Append(string.Format("<form id=\"{0}\" name=\"{0}\" action=\"{1}\" method=\"POST\">", formId, url));
            foreach (var item in postData)
            {
                strForm.Append($"<input type=\"hidden\" name=\"{item.Key}\" value=\"{item.Value}\"/>");
            }
            strForm.Append("</form>");

            StringBuilder strScript = new StringBuilder();
            strScript.Append("<script language=\"javascript\">");
            strScript.Append(string.Format("var v{0}=document.{0};", formId));
            strScript.Append($"v{formId}.submit();");
            strScript.Append("</script>");

            return strForm + strScript.ToString();
        }
    }
}
