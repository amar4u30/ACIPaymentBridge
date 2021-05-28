using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Cmc.Core.PaymentProvider.AciPaymentBridge
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Property)]
    internal class HttpParameterNameAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpParameterNameAttribute"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public HttpParameterNameAttribute(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return Name;
        }
    }

    /// <summary>
    /// This Dto contains information related to response data received from Aci calls.
    /// Each property in this class corresponds to Name-Value pair of data content received from POST calls
    /// </summary>
    internal class PaymentResponseDto
    {
        /// <summary>
        /// Gets or sets the Bill To FirstName.
        /// </summary>
        /// <value>
        /// TheBill To FirstName.
        /// </value>
        [HttpParameterName("BillToFirstName")]
        public string Firstname { get; set; }
        /// <summary>
        /// Gets or sets the Bill To LastName.
        /// </summary>
        /// <value>
        /// The Bill To LastName.
        /// </value>
        [HttpParameterName("BillToLastName")]
        public string Lastname { get; set; }
        /// <summary>
        /// Gets or sets the secure token.
        /// </summary>
        /// <value>
        /// The secure token.
        /// </value>
        [HttpParameterName("SecureToken")]
        public string SecureToken { get; set; }
        /// <summary>
        /// Gets or sets the country to ship.
        /// </summary>
        /// <value>
        /// The country to ship.
        /// </value>
        [HttpParameterName("CountryToSip")]
        public string CountryToShip { get; set; }
        /// <summary>
        /// Gets or sets the Address Verification System Code.
        /// </summary>
        /// <value>
        /// The avs data.
        /// </value>
        [HttpParameterName("Avsdata")]
        public string AvsData { get; set; }
        /// <summary>
        /// Gets or sets the amount.
        /// </summary>
        /// <value>
        /// The amount.
        /// </value>
        [HttpParameterName("Amt")]
        public decimal Amount { get; set; }
        /// <summary>
        /// Gets or sets the tax.
        /// </summary>
        /// <value>
        /// The tax.
        /// </value>
        [HttpParameterName("Tax")]
        public decimal Tax { get; set; }
        /// <summary>
        /// Gets or sets the Approval Code.
        /// </summary>
        /// <value>
        /// The authcode.
        /// </value>
        [HttpParameterName("Authcode")]
        public string Authcode { get; set; }
        /// <summary>
        /// Gets or sets the tender.
        /// </summary>
        /// <value>
        /// The tender.
        /// </value>
        [HttpParameterName("Tender")]
        public string Tender { get; set; }
        /// <summary>
        /// Gets or sets the result.
        /// </summary>
        /// <value>
        /// The result.
        /// </value>
        [HttpParameterName("Result")]
        public int Result { get; set; }
        /// <summary>
        /// Gets or sets the Response Message for a given executed payment transaction.
        /// </summary>
        /// <value>
        /// The resp MSG.
        /// </value>
        [HttpParameterName("RespMsg")]
        public string ResponseMessage { get; set; }
        /// <summary>
        /// Gets or sets the Pre-Processing Fraud Protection Service Message.
        /// </summary>
        /// <value>
        /// The prefpmsg.
        /// </value>
        [HttpParameterName("Prefpsmsg")]
        public string PreFpsMessage { get; set; }
        /// <summary>
        /// Gets or sets the Post-Processing Fraud Protection Service Message.
        /// </summary>
        /// <value>
        /// The post fpmessage.
        /// </value>
        [HttpParameterName("PostFpsmsg")]
        public string PostFpsMessage { get; set; }
        /// <summary>
        /// Gets or sets the bill to address1 + address2.
        /// </summary>
        /// <value>
        /// The bill to address1 + address2.
        /// </value>
        [HttpParameterName("BILLTOSTREET")]
        public string BillToAddress1 { get; set; }
        /// <summary>
        /// Gets or sets the bill to city.
        /// </summary>
        /// <value>
        /// The bill to city.
        /// </value>
        [HttpParameterName("BILLTOCITY")]
        public string BillToCity { get; set; }
        /// <summary>
        /// Gets or sets the bill to zip.
        /// </summary>
        /// <value>
        /// The bill to zip.
        /// </value>
        [HttpParameterName("BILLTOZIP")]
        public string BillToZip { get; set; }
        /// <summary>
        /// Gets or sets the bill to state.
        /// </summary>
        /// <value>
        /// The bill to state.
        /// </value>
        [HttpParameterName("BILLTOSTATE")]
        public string BillToState { get; set; }
        /// <summary>
        /// Gets or sets the bill to country.
        /// </summary>
        /// <value>
        /// The bill to country.
        /// </value>
        /// <summary>
        /// Gets or sets the bill to country.
        /// </summary>
        /// <value>
        /// The bill to country.
        /// </value>
        [HttpParameterName("BillToCountry")]
        public string BillToCountry { get; set; }
        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        /// <value>
        /// The country.
        /// </value>
        [HttpParameterName("Country")]
        public string Country { get; set; }
        /// <summary>
        /// Gets or sets the alpha numeric Transaction Code of a payment transaction executed.
        /// For Aci, this field contains the funding tokenId value and 
        /// for IATS, this contrains the TransactionID
        /// </summary>
        /// <value>
        /// The pnref.
        /// </value>
        [HttpParameterName("tokenId")]
        public string TransactionCode { get; set; }
        /// <summary>
        /// Gets or sets the last four digits of credit card.
        /// For Aci, this field contains the fundingAccount value and 
        /// for IATS, this contrains the ACCT
        /// </summary>
        /// <value>
        /// The ACCT.
        /// </value>
        [HttpParameterName("fundingAccount")]
        public string LastFour { get; set; }
        /// <summary>
        /// Gets or sets the type of credit card.
        /// For Aci, this field contains the fundingAccountSubType value and 
        /// for IATS, this contrains the CARDTYPE
        /// </summary>
        /// <value>
        /// The CARDTYPE.
        /// </value>
        [HttpParameterName("fundingAccountSubType")]
        public string CardType { get; set; }
        /// <summary>
        /// Gets or sets the credit card expiry in mmyy format.
        /// For Aci, this field contains the expiryDate value and 
        /// for IATS, this contrains the EXPDATE
        /// </summary>
        /// <value>
        /// The pnref.
        /// </value>
        [HttpParameterName("expiryDate")]
        public string ExpiryMonthYear { get; set; }
        /// <summary>
        /// Gets or sets the ship to country.
        /// </summary>
        /// <value>
        /// The ship to country.
        /// </value>
        [HttpParameterName("ShipToCountry")]
        public string ShipToCountry { get; set; }
        /// <summary>
        /// Gets or sets the transaction time.
        /// </summary>
        /// <value>
        /// The transaction time.
        /// </value>
        [HttpParameterName("TransTime")]
        public DateTime TransactionTime { get; set; }
        /// <summary>
        /// Gets or sets the hostcode.
        /// </summary>
        /// <value>
        /// The hostcode.
        /// </value>
        [HttpParameterName("HostCode")]
        public string Hostcode { get; set; }
        /// <summary>
        /// Gets or sets The Fraud Protection Service Pre-Processing XML data in case if any Fraud Prevention rule gets triggered.
        /// </summary>
        /// <value>
        /// The Fraud Protection Service Pre-Processing XML data in case if any Fraud Prevention rule gets triggered .
        /// </value>
        [HttpParameterName("Fps_Prexmldata")]
        public string FpsPrexmlData { get; set; }
        /// <summary>
        /// Gets or sets the user1.
        /// </summary>
        /// <value>
        /// The user1.
        /// </value>
        [HttpParameterName("User1")]
        public string UserDefinedField1 { get; set; }
        /// <summary>
        /// Gets or sets the user2.
        /// </summary>
        /// <value>
        /// The user2.
        /// </value>
        [HttpParameterName("User2")]
        public string UserDefinedField2 { get; set; }
        /// <summary>
        /// Gets or sets the user3.
        /// </summary>
        /// <value>
        /// The user3.
        /// </value>
        [HttpParameterName("User3")]
        public string UserDefinedField3 { get; set; }
        /// <summary>
        /// Gets or sets the user4.
        /// </summary>
        /// <value>
        /// The user4.
        /// </value>
        [HttpParameterName("User4")]
        public string UserDefinedField4 { get; set; }
        /// <summary>
        /// Gets or sets the user5.
        /// </summary>
        /// <value>
        /// The user5.
        /// </value>
        [HttpParameterName("User5")]
        public string UserDefinedField5 { get; set; }
        /// <summary>
        /// Gets or sets the user6.
        /// </summary>
        /// <value>
        /// The user6.
        /// </value>
        [HttpParameterName("User6")]
        public string UserDefinedField6 { get; set; }
        /// <summary>
        /// Gets or sets the user7.
        /// </summary>
        /// <value>
        /// The user7.
        /// </value>
        [HttpParameterName("User7")]
        public string UserDefinedField7 { get; set; }
        /// <summary>
        /// Gets or sets the user8.
        /// </summary>
        /// <value>
        /// The user8.
        /// </value>
        [HttpParameterName("User8")]
        public string UserDefinedField8 { get; set; }
        /// <summary>
        /// Gets or sets the user9.
        /// </summary>
        /// <value>
        /// The user9.
        /// </value>
        [HttpParameterName("User9")]
        public string UserDefinedField9 { get; set; }
        /// <summary>
        /// Gets or sets the user10.
        /// </summary>
        /// <value>
        /// The user10.
        /// </value>
        [HttpParameterName("User10")]
        public string UserDefinedField10 { get; set; }


    }

    /// <summary>
    /// This Dto contains information related to request data to be sent to Aci.
    /// </summary>
    internal class PaymentRequstDto
    {
        /// <summary>
        /// Gets or sets the productId.
        /// </summary>
        /// <value>
        /// The type of the transaction.
        /// </value>
        [HttpParameterName("productID")]
        public string ProductId { get; set; }        
        /// <summary>
        /// Gets or sets the Custom Parameter for the transaction.
        /// </summary>
        /// <value>
        /// The type of the transaction.
        /// </value>
        //[HttpParameterName("cde-paymentIdentifier-0")]
        //public string cdepaymentIdentifier0 { get; set; }        
        /// <summary>
        /// Gets or sets the Payment amount .
        /// </summary>
        /// <value>
        /// The type of the transaction.
        /// </value>
        [HttpParameterName("paymentAmount")]
        public decimal PaymentAmount { get; set; }        
        /// <summary>
        /// Gets or sets the Payee First Name.
        /// </summary>
        /// <value>
        /// The type of the transaction.
        /// </value>
        [HttpParameterName("firstName")]
        public string FirstName { get; set; }        
        /// <summary>
        /// Gets or sets the Payee Middle Name.
        /// </summary>
        /// <value>
        /// The type of the transaction.
        /// </value>
        [HttpParameterName("middleName")]
        public string MiddleName { get; set; }        
        /// <summary>
        /// Gets or sets the Payee Last Name.
        /// </summary>
        /// <value>
        /// The type of the transaction.
        /// </value>
        [HttpParameterName("lastName")]
        public string LastName { get; set; }
        /// <summary>
        /// Gets or sets the Suffix.
        /// </summary>
        /// <value>
        /// The type of the transaction.
        /// </value>
        [HttpParameterName("suffix")]
        public string Suffix { get; set; }
        /// <summary>
        /// Gets or sets the Payee Address.
        /// </summary>
        /// <value>
        /// The type of the transaction.
        /// </value>
        [HttpParameterName("address1")]
        public string Address1 { get; set; }        
        /// <summary>
        /// Gets or sets the Payee City name.
        /// </summary>
        /// <value>
        /// The type of the transaction.
        /// </value>
        [HttpParameterName("cityName")]
        public string CityName { get; set; }
        /// <summary>
        /// Gets or sets the Payee Province.
        /// </summary>
        /// <value>
        /// The type of the transaction.
        /// </value>
        [HttpParameterName("provinceCd")]
        public string ProvinceCd { get; set; }
        /// <summary>
        /// Gets or sets the Payee Postal code.
        /// </summary>
        /// <value>
        /// The type of the transaction.
        /// </value>
        [HttpParameterName("postalCd")]
        public string PostalCd { get; set; }        
        /// <summary>
        /// Gets or sets the Payee Phone number.
        /// </summary>
        /// <value>
        /// The type of the transaction.
        /// </value>
        [HttpParameterName("phoneNum")]
        public string PhoneNum { get; set; }       
        /// <summary>
        /// Gets or sets the Payee Email ID.
        /// </summary>
        /// <value>
        /// The type of the transaction.
        /// </value>
        [HttpParameterName("email")]
        public string Email { get; set; }        
        /// <summary>
        /// Gets or sets the Return url for the transaction.
        /// </summary>
        /// <value>
        /// The type of the transaction.
        /// </value>
        [HttpParameterName("returnUrl")]
        public string ReturnUrl { get; set; }
        /// <summary>
        /// Gets or sets the Postback url for the transaction.
        /// </summary>
        /// <value>
        /// The type of the transaction.
        /// </value>
        [HttpParameterName("postbackUrl")]
        public string PostbackUrl { get; set; }
        /// <summary>
        /// Gets or sets the Error url for the transaction.
        /// </summary>
        /// <value>
        /// The type of the transaction.
        /// </value>
        [HttpParameterName("errorUrl")]
        public string ErrorUrl { get; set; }
        /// <summary>
        /// Gets or sets the Cancel url for the transaction.
        /// </summary>
        /// <value>
        /// The type of the transaction.
        /// </value>
        [HttpParameterName("cancelUrl")]
        public string CancelUrl { get; set; }        
        /// <summary>
        /// Gets or sets the flag to enable/disbale editing amount.
        /// </summary>
        /// <value>
        /// The type of the transaction.
        /// </value>
        [HttpParameterName("editAmount")]
        public bool EditAmount { get; set; }        
        /// <summary>
        /// Gets or sets the productId.
        /// </summary>
        /// <value>
        /// The type of the transaction.
        /// </value>
        [HttpParameterName("lockAmount")]
        public bool LockAmount { get; set; }       
        /// <summary>
        /// Gets or sets the Payment Options like Credit Card.
        /// </summary>
        /// <value>
        /// The type of the transaction.
        /// </value>
        [HttpParameterName("payBy")]
        public string PayBy { get; set; }        
        /// <summary>
        /// Gets or sets the productId.
        /// </summary>
        /// <value>
        /// The type of the transaction.
        /// </value>
        [HttpParameterName("paymentmethoddisplay")]
        public string Paymentmethoddisplay { get; set; }
    }
    public class AciPaymentBridgeDataTypes
    {
    }
}
