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
                    GetImage();
                })
            });
        }

        async void GetImage()
        {
            if (ViewModel.IsBusy)
                return;

            ViewModel.IsBusy = true;

            bool confirm = await DisplayAlert("Profile", "", "Camera", "Sélectionner dans Gallery");
            var pickedImage = confirm ? await MyCrossMediaImp.TakeImage() : await MyCrossMediaImp.PickImage();
            if (pickedImage != null)
                this.ImageProfile.Source = pickedImage;

            ViewModel.IsBusy = false;
        }
    }
}