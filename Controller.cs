// Product side code to call Payment gateway.
if (model.PaymentProvider.Equals("ACI", StringComparison.OrdinalIgnoreCase))
{
  var config = ServiceLocator.Default.GetInstance<IConfigurationManager>("EngageConfigurationProvider");
  config.AppSettings.Set("PaymentProvider", "ACIPaymentBridge");
  Log.Write("ACI payment ");
  payload.PaymentRequest.UserDefinedField3 = model.ReturnUrl;

  payload.PaymentRequest.ReturnUrl = GetBaseUrl() + $"Core/api/Payment/PaymentReceipt/return?User1={payload.PaymentRequest.UserDefinedField1}&User2={payload.PaymentRequest.UserDefinedField2}&User3={payload.PaymentRequest.UserDefinedField3}&User10={payload.PaymentRequest.CorrelationId}";
  payload.PaymentRequest.ErrorUrl = payload.PaymentRequest.ErrorUrl + $"?status=failure&User1={payload.PaymentRequest.UserDefinedField1}&User2={payload.PaymentRequest.UserDefinedField2}&User3={payload.PaymentRequest.UserDefinedField3}&User10={payload.PaymentRequest.CorrelationId}";
  payload.PaymentRequest.CancelUrl = payload.PaymentRequest.CancelUrl + $"?status=failure&User1={payload.PaymentRequest.UserDefinedField1}&User2={payload.PaymentRequest.UserDefinedField2}&User3={payload.PaymentRequest.UserDefinedField3}&User10={payload.PaymentRequest.CorrelationId}";

  payload.PaymentRequest.ProviderInfo = new PaymentProviderInfo()
  {
     MerchantCode = configuration.Attributes["cmc_paymentgatewayaciproductid"]?.ToString(),
     HostedPageUrl = configuration.Attributes["cmc_paymentgatewayacihostedurl"]?.ToString(),
  };
  model.PaymentProvider = "ACIPaymentBridge";
  Log.Write("Payment provider Name: " + model.PaymentProvider);                    
}               
