using System;
using System.Collections.Generic;
using System.Text;

namespace Starbucks
{
    public class ImageGetter : BaseGetter
    {
        public async Task<string> GetImage()
        {
            var postdata = @"<?xml version=""1.0"" encoding=""utf-8""?><soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/""><soap:Body><GetCardImage xmlns=""http://plustech/giftcardwebprocessor""><login>iphone_app</login><password>Plas-Bucks</password><cardNumber>728010171859</cardNumber><imageWidth>270</imageWidth><imageHeight>170</imageHeight><imageNumber>0</imageNumber></GetCardImage></soap:Body></soap:Envelope>";
            var data = Execute<string>("GiftCardService/GiftCardWeb.asmx", postdata);

            return "";
        }
    }
}
