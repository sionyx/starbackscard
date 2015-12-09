using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Starbucks.Models;

#region response
//<?xml version="1.0" encoding="utf-8"?>
//<soap:Envelope xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
//<soap:Body>
//    <GetCardInfoResponse xmlns="http://plustech/giftcardwebprocessor">
//        <balance>1634</balance>
//        <maxAddAmount>8366.01</maxAddAmount>
//    </GetCardInfoResponse>
//</soap:Body>
//</soap:Envelope>
#endregion

namespace Starbucks.Getters
{
    public class BalanceGetter : BaseGetter
    {
        public async Task<string> GetBalance(CardData cardData)
        {
            var postdata = string.Format(@"<?xml version=""1.0"" encoding=""utf-8""?><soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/""><soap:Body><GetCardInfo xmlns=""http://plustech/giftcardwebprocessor""><login>iphone_app</login><password>Plas-Bucks</password><cardNumber>{0}</cardNumber><pin>{1}</pin></GetCardInfo></soap:Body></soap:Envelope>", cardData.Number, cardData.Code);
            var xmlbody = await Execute("http://plustech/giftcardwebprocessor/GetCardInfo", postdata);

            if (xmlbody == null)
                return null;

            var ns = XNamespace.Get("http://plustech/giftcardwebprocessor");//  xmlbody.Name.Namespace;
            var response = xmlbody.Element(ns + "GetCardInfoResponse");
            var balance = response.Element(ns + "balance").Value;
            //var maxAddAmunt = response.Element(ns + "maxAddAmount").Value;

            return balance;            
        }
    }
}
