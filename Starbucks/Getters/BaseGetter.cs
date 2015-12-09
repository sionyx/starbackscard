using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.Security.Cryptography.Certificates;
using Windows.Web.Http;
using Windows.Web.Http.Filters;
using UnicodeEncoding = Windows.Storage.Streams.UnicodeEncoding;

namespace Starbucks.Getters
{
    public class BaseGetter
    {
        private HttpClient _client;

        protected async Task<XElement> Execute(string command, string postdata)
        {
            try
            {
                if (_client == null)
                {
                    var filter = new HttpBaseProtocolFilter();
                    filter.IgnorableServerCertificateErrors.Add(ChainValidationResult.Untrusted);
                    filter.IgnorableServerCertificateErrors.Add(ChainValidationResult.Expired);

                    _client = new HttpClient(filter);
                    _client.DefaultRequestHeaders.Accept.ParseAdd("*/*");
                    _client.DefaultRequestHeaders.AcceptEncoding.ParseAdd("gzip, deflate");
                    _client.DefaultRequestHeaders.AcceptLanguage.ParseAdd("ru");
                    _client.DefaultRequestHeaders.UserAgent.ParseAdd("StarbucksCard/1.0.7 CFNetwork/711.2.23 Darwin/14.0.0");
                    _client.DefaultRequestHeaders.Add("SOAPAction", command);
                }

                var content = new HttpStringContent(postdata, UnicodeEncoding.Utf8, "text/xml");

                var response = await _client.PostAsync(new Uri("https://194.135.22.144/GiftCardService/GiftCardWeb.asmx"), content);
                if (response.IsSuccessStatusCode)
                {
                    var resultStr = await response.Content.ReadAsStringAsync();
                    var resultXml = XElement.Parse(resultStr);
                    var ns = resultXml.Name.Namespace;
                    var body = resultXml.Element(ns + "Body");

                    return body;
                }
            }
            catch (Exception ex)
            {

            }

            return null;
        }

    }
}
