using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Media;

namespace SenEvents
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpPage : ContentPage
    {
        SignUpViewModel ViewModel;

        public SignUpPage()
        {
            InitializeComponent();

            this.BindingContext = ViewModel = new SignUpViewModel();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            this.PickerCity.ItemsSource = this.ViewModel.CityList;

            this.ImageProfile.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() =>
                {
                    PickImage();
                })
            });
        }

        async void PickImage()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Photos non supportées", ":( Permission not granted to photos.", "OK");
                return;
            }
            var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,
            });

            if (file == null)
                return;

            ImageProfile.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                file.Dispose();
                return stream;
            });

            IsBusy = false;
        }
    }
}