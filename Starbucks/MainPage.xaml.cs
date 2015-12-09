using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;
using Microsoft.AdMediator.Core.Events;

namespace Starbucks
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage
    {

        public MainPage()
        {
            InitializeComponent();
            Loaded += OnLoaded;
            NavigationCacheMode = NavigationCacheMode.Required;
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            SetupAdMediator();
        }

        private void SetupAdMediator()
        {
            if (ActualWidth >= 640)
            {
                AdMediator_C9D4A2.Width = 640;
                AdMediator_C9D4A2.Height = 100;
            }
            else if (ActualWidth >= 480)
            {
                AdMediator_C9D4A2.Width = 480;
                AdMediator_C9D4A2.Height = 80;
            }
            else if (ActualWidth >= 400)
            {
                AdMediator_C9D4A2.Width = 400;
                AdMediator_C9D4A2.Height = 67;
            }
            else if (ActualWidth >= 320)
            {
                AdMediator_C9D4A2.Width = 320;
                AdMediator_C9D4A2.Height = 50;
            }
            else
            {
                AdMediator_C9D4A2.Width = 300;
                AdMediator_C9D4A2.Height = 50;
            }
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }

        private void AdMediatorControl_OnAdSdkError(object sender, AdFailedEventArgs e)
        {
            Debug.WriteLine("AdMediatorError:" + e.Error + " " + e.ErrorCode);
        }

        private void AdMediatorControl_OnAdSdkEvent(object sender, AdSdkEventArgs e)
        {
            Debug.WriteLine("AdSdk event {0} by {1}", e.EventName, e.Name);
        }

        private void AdMediatorControl_OnAdMediatorFilled(object sender, AdSdkEventArgs e)
        {
            Debug.WriteLine("AdFilled:" + e.Name);
        }

        private void AdMediatorControl_OnAdMediatorError(object sender, AdMediatorFailedEventArgs e)
        {
            Debug.WriteLine("AdSdkError ErrorCode: {0} ErrorMessage: {1} Error: {2}", e.ErrorCode, e.Error.Message, e.Error);
        }







        //        public async Task<string> GetImage()
        //        {
        //            var postdata = @"<?xml version=""1.0"" encoding=""utf-8""?><soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/""><soap:Body><GetCardImage xmlns=""http://plustech/giftcardwebprocessor""><login>iphone_app</login><password>Plas-Bucks</password><cardNumber>728010171859</cardNumber><imageWidth>270</imageWidth><imageHeight>170</imageHeight><imageNumber>0</imageNumber></GetCardImage></soap:Body></soap:Envelope>";
        //            var data = Execute<string>("http://plustech/giftcardwebprocessor/GetCardImage", postdata);
        //
        //            // <?xml version="1.0" encoding="utf-8"?><soap:Envelope xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema"><soap:Body><GetCardImageResponse xmlns="http://plustech/giftcardwebprocessor"><DataOutCount>2</DataOutCount><DataOut>;728010170349#728010198522</DataOut></GetCardImageResponse></soap:Body></soap:Envelope>
        //
        //            return "";
        //        }

    }
}
