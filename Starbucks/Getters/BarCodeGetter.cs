using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Starbucks.Models;

#region response
//<?xml version="1.0" encoding="utf-8"?>
//            <soap:Envelope xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
//            <soap:Body>
//                <GetCardCodeResponse xmlns="http://plustech/giftcardwebprocessor">
//                    <code>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</code>
//                    <DataOutCount>0</DataOutCount>
//                </GetCardCodeResponse>
//            </soap:Body></soap:Envelope>
#endregion

namespace Starbucks.Getters
{
    public class BarCodeGetter : BaseGetter
    {
        public async Task<byte[]> GetBarCodeData(CardData card)
        {
            var postdata = string.Format(@"<?xml version=""1.0"" encoding=""utf-8""?><soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/""><soap:Body><GetCardCode xmlns=""http://plustech/giftcardwebprocessor""><login>iphone_app</login><password>Plas-Bucks</password><cardNumber>{0}</cardNumber><pin>{1}</pin></GetCardCode></soap:Body></soap:Envelope>", card.Number, card.Code);
            var xmlbody = await Execute("http://plustech/giftcardwebprocessor/GetCardCode", postdata);

            if (xmlbody == null)
                return null;

            var ns = XNamespace.Get("http://plustech/giftcardwebprocessor");//  xmlbody.Name.Namespace;
            var response = xmlbody.Element(ns + "GetCardCodeResponse");
            var code = response.Element(ns + "code");
            var codestring = code.Value;

            var data = Convert.FromBase64String(codestring);

            return data;
        }
    }
}
