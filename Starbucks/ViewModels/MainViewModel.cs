using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Storage.Streams;
using Windows.UI.Core;
using Windows.UI.Popups;
//using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Starbucks.Common;
using Starbucks.Getters;
using Starbucks.Models;
using Starbucks.Services;
using Starbucks.Views;

namespace Starbucks.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private CardData _card;
        private readonly CardsStorage _cardsStorage;
        private readonly DataStorage _dataStorage;
        private readonly CoreDispatcher _dispatcher;

        public MainViewModel(CoreDispatcher dispatcher)
        {
            _cardsStorage = new CardsStorage();
            _dataStorage = new DataStorage();
            _dispatcher = dispatcher;

            CardTapCommand = new RelayCommand(AuthAndLoad);
            RefreshCommand = new RelayCommand(UpdateBalance);
            DeleteCardCommand = new RelayCommand(AskDeleteCard);

            Load();
        }

        private void Load()
        {
            _card = _cardsStorage.GetCardData();

            UpdateBalance();
            UpdateImage();
        }

        private async void UpdateBalance()
        {
            if (_card == null)
            {
                Balance = "0";
                return;
            }

            var getter = new BalanceGetter();
            var balance = await getter.GetBalance(_card);

            Balance = balance ?? "0";
        }

        private async void UpdateImage()
        {
            if (_card == null) return;

            var data = await _dataStorage.LoadGifFile(_card.Number);

            if (data == null)
            {
                var getter = new BarCodeGetter();
                data = await getter.GetBarCodeData(_card);

                if (data != null)
                {
                    _dataStorage.SaveGifFile(data, _card.Number);
                }
            }

            if (data == null) return;

            BarCode = await CreateImageBitmapAsync(data);
        }

        private async void AuthAndLoad()
        {
            if (_card != null)
            {
                Opened = !Opened;
                return;
            }


            var dialog = new AuthPage();
            var res = await dialog.ShowAsync();

            if (res != ContentDialogResult.Primary) return;

            _card = dialog.CardData;

            //number = "728010171859";
            //code = "900146";

            InProgress = true;

            var getter = new BarCodeGetter();
            var data = await getter.GetBarCodeData(_card);
            if (data == null)
            {
                var messageDialog = new MessageDialog("Не удалось получить данные карты. Проверьте данные и попробуйте снова");
                await messageDialog.ShowAsync();
                return;
            }

            _cardsStorage.StoreCardData(_card);
            _dataStorage.SaveGifFile(data, _card.Number);
            BarCode = await CreateImageBitmapAsync(data);
            Opened = true;

            InProgress = false;

            UpdateBalance();
        }

        private async void AskDeleteCard()
        {
            if (_card == null)
                return;

            var messageDialog = new MessageDialog("Вы уверены, что хотите удалить данную карту?");
            messageDialog.Commands.Add(new UICommand("Удалить", command => DeleteCard()));
            messageDialog.Commands.Add(new UICommand("Оставить"));
            await messageDialog.ShowAsync();
        }

        private async void DeleteCard()
        {
            InProgress = true;

            await _dataStorage.DeleteGifFile(_card.Number);
            _cardsStorage.DeleteCardData(_card);
            BarCode = null;
            Balance = "0";
            _card = null;

            await Task.Delay(500);

            InProgress = false;

            Opened = false;
        }

        private async Task<BitmapImage> CreateImageBitmapAsync(byte[] data)
        {
            BitmapImage bmp = null;
            await _dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
            {
                var stream = new InMemoryRandomAccessStream();
                await stream.WriteAsync(data.AsBuffer());
                stream.Seek(0);
                bmp = new BitmapImage();
                await bmp.SetSourceAsync(stream);
            });

            return bmp;
        }


        private string _balance;
        public string Balance 
        {
            get { return _balance; }
            set
            {
                _balance = value;
                OnPropertyChanged();
            }
        }

        private BitmapImage _barCode;
        public BitmapImage BarCode
        {
            get { return _barCode; }
            set
            {
                _barCode = value;
                OnPropertyChanged();
            }
        }

        private bool _opened;
        public bool Opened
        {
            get { return _opened; }
            set
            {
                _opened = value;
                OnPropertyChanged();
            }
        }

        public ICommand CardTapCommand { get; set; }
        public ICommand RefreshCommand { get; set; }
        public ICommand DeleteCardCommand { get; set; }
    }
}
