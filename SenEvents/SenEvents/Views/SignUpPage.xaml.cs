using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
            {
                this.ImageProfile.Source = pickedImage.ImageSource;
                ViewModel.pickedImagePath = pickedImage.Path;
            }

            ViewModel.IsBusy = false;
        }

        async void ButtonSignUp_Clicked(object sender, EventArgs e)
        {
            if (ViewModel.IsBusy)
                return;
            ViewModel.IsBusy = true;

            if (!string.IsNullOrEmpty(ViewModel.pickedImagePath))
            {
                ViewModel.ImageStore.upload(ViewModel.pickedImagePath);
                return;
            }

            string name = this.EntryName.Text,
                email = this.EntryEmail.Text,
                city = (string)this.PickerCity.SelectedItem,
                password = this.EntryPassword.Text,
                confirmPassword = this.EntryConfirmPassword.Text;

            if (!Validators.checkEmail(email))
            {
                await DisplayAlert("Erreur", "Email invalide", "OK");
                return;
            }

            if (!Validators.checkPasswordValide(password))
            {
                await DisplayAlert("Erreur", "Mot de passe invalide", "OK");
                return;
            }

            if (!Validators.checkConfirmationPassword(password, confirmPassword))
            {
                await DisplayAlert("Erreur", "La confirmation est le mot de passe sont différent", "OK");
                return;
            }

            if (await ViewModel.UserStore.UserExistAsync(email))
            {
                await DisplayAlert("Erreur", "Cet utilisateur existe dèja", "OK");
                return;
            }

            ViewModel.IsBusy = false;
        }
    }
}