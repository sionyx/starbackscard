using Windows.UI.Core;
using Starbucks.ViewModels;

namespace Starbucks
{
    public class CompositionRoot
    {
        private MainViewModel _mainViewModel;
        public MainViewModel MainViewModel => _mainViewModel ?? (_mainViewModel = new MainViewModel(Dispatcher));

        private CoreDispatcher _dispatcher;
        public CoreDispatcher Dispatcher => _dispatcher ?? (_dispatcher = CoreWindow.GetForCurrentThread().Dispatcher);
    }
}
