using System;
using System.Collections.Generic;
using System.Reflection;
using AutoMapper;
using Cmc.Core.ComponentModel;
using Cmc.Core.PaymentProvider.Interfaces;
using Cmc.Core.PaymentProvider.Messages;

namespace Cmc.Core.PaymentProvider.AciPaymentBridge
{
    /// <summary>
    /// Class reponsible for converting the Dictionary of 
    /// payment transaction data, received from the Payment Controller,
    /// to PaymentTransactionResponse message
    /// </summary>
    /// <seealso cref="Cmc.Core.PaymentProvider.Interfaces.IPaymentTransactionResponseConverter" />
    [Injectable(InstanceScope.Factory)]
    internal class AciPaymentBridgeTransactionResponseConverter : IPaymentTransactionResponseConverter
    {
        /// <summary>
        /// The _mapper transaction response
        /// </summary>
        private IMapper _mapperTransactionResponse;

        /// <summary>
        /// The _mapper transaction response exception
        /// </summary>
        private IMapper _mapperTransactionResponseException;

        /// <summary>
        /// Initializes a new instance of the <see cref="AciPaymentBridgeTransactionResponseConverter"/> class.
        /// </summary>
        public AciPaymentBridgeTransactionResponseConverter()
        {
            InitializeMapper();
        }
        /// <summary>
        /// Initializes the mapper.
        /// </summary>
        private void InitializeMapper()
        {

            _mapperTransactionResponse = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<PaymentResponseDto, PaymentTransactionResponse>();

            }).CreateMapper();

            _mapperTransactionResponseException = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<PaymentResponseDto, PaymentTransactionResponse>()
                    .ForMember(dest => dest.PaymentException, conf =>
                    {
                        conf.Condition(src => src.Result != 10);
                        conf.MapFrom(src =>

                            new PaymentException { ExceptionCode = src.Result, ExceptionMessage = src.ResponseMessage }
                        );
                    });
            }).CreateMapper();

        }

        /// <summary>
        /// Converts to transaction response.
        /// </summary>
        /// <param name="transactionData">The transaction data.</param>
        /// <returns></returns>
        public PaymentTransactionResponse ConvertToTransactionResponse(IDictionary<string, object> transactionData)
        {

            var responseDto = ConvertToPaymentResponseDto<PaymentResponseDto>(transactionData);
            var transactionResponse =
                _mapperTransactionResponse.Map<PaymentResponseDto, PaymentTransactionResponse>(responseDto);
            transactionResponse = _mapperTransactionResponseException.Map(responseDto, transactionResponse);
            object statusVal = null;
            if (transactionResponse.PaymentException.ExceptionCode == 0 && transactionData.TryGetValue("status", out statusVal))
            {
                if (statusVal != null && statusVal.ToString().ToLower() == "failure")
                {
                    object reasonVal = null;
                    transactionResponse.PaymentException.ExceptionCode = -1;
                    transactionData.TryGetValue("reason", out reasonVal);
                    transactionResponse.PaymentException.ExceptionMessage = reasonVal != null ? reasonVal.ToString() : "CMC: Exception message not received from Provider. Please check the transaction with the provider.";
                }
            }

            return transactionResponse;
        }

        /// <summary>
        /// Converts to Payment response dto.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        private T ConvertToPaymentResponseDto<T>(IDictionary<string, object> data) where T : class, new()
        {

            var response = new T();
            PropertyInfo[] properties = typeof(T).GetProperties();
            if (data == null)
                return response;

            #region commented code
            foreach (var propertyInfo in properties)
            {
                HttpParameterNameAttribute customAttrib =
                    (HttpParameterNameAttribute)Attribute.GetCustomAttribute(propertyInfo,
                        typeof(HttpParameterNameAttribute));
                string parameterName = (customAttrib != null) ? customAttrib.Name : propertyInfo.Name;

                object value;

                if (data.TryGetValue(parameterName, out value))
                {
                    if (propertyInfo.PropertyType == typeof(int))
                    {
                        propertyInfo.SetValue(response, Convert.ToInt32(value));
                    }
                    else if (propertyInfo.PropertyType == typeof(decimal))
                    {
                        propertyInfo.SetValue(response, Convert.ToDecimal(value));
                    }
                    else if (propertyInfo.PropertyType == typeof(DateTime))
                    {
                        propertyInfo.SetValue(response, Convert.ToDateTime(value));
                    }
                    else
                    {
                        //if (parameterName.Equals("fundingaccountsubtype", StringComparison.InvariantCultureIgnoreCase))
                        //{
                        //    switch (value.ToString().ToLowerInvariant())
                        //    {
                        //        case "visa":
                        //            value = "Visa";
                        //            break;
                        //        case "mc":
                        //            value = "MasterCard";
                        //            break;
                        //        case "disc":
                        //            value = "Discover";
                        //            break;
                        //        case "amex":
                        //            value = "American Express";
                        //            break;
                        //        default:
                        //            value = "Other";
                        //            break;
                        //    }
                        //}
                        //else if (parameterName.Equals("fundingAccount", StringComparison.OrdinalIgnoreCase))
                        //{
                        //    if (value.ToString().Contains("*"))
                        //        value = value.ToString().Replace("*", "");
                        //}
                        propertyInfo.SetValue(response, value);
                    }
                }
            }

            #endregion

            return response;
        }
    }
}
