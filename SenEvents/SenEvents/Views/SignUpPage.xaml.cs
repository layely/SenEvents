using Plugin.FileUploader.Abstractions;
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


            string name = this.EntryName.Text,
                email = this.EntryEmail.Text,
                city = (string)this.PickerCity.SelectedItem,
                password = this.EntryPassword.Text,
                confirmPassword = this.EntryConfirmPassword.Text;

            if (!Validators.checkEmail(email))
            {
                await DisplayAlert("Erreur", "Email invalide", "OK");
                ViewModel.IsBusy = false;
                return;
            }

            if (!Validators.checkPasswordValide(password))
            {
                await DisplayAlert("Erreur", "Mot de passe invalide", "OK");
                ViewModel.IsBusy = false;
                return;
            }

            if (!Validators.checkConfirmationPassword(password, confirmPassword))
            {
                await DisplayAlert("Erreur", "La confirmation est le mot de passe sont différent", "OK");
                ViewModel.IsBusy = false;
                return;
            }

            if (await ViewModel.UserStore.UserExistAsync(email))
            {
                await DisplayAlert("Erreur", "Cet utilisateur existe dèja", "OK");
                ViewModel.IsBusy = false;
                return;
            }

            if (!string.IsNullOrEmpty(ViewModel.pickedImagePath))
            {
                ProgressBar.Progress = 0;
                ProgressBar.IsVisible = true;
                // Note that here the upload result will manage to reset the busy-state to false once complete.
                ViewModel.ImageStore.upload(ViewModel.pickedImagePath, Current_FileUploadCompleted, Current_FileUploadError, Current_FileUploadProgress);
                return;
            }

            // In case the user does not take or pick a photo-profile 
            await Task.Run(() => saveUser(string.Empty));

            ViewModel.IsBusy = false;
        }

        private void Current_FileUploadProgress(object sender, FileUploadProgress e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                ProgressBar.ProgressTo(e.Percentage / 100.0f, 100, Easing.Linear);
            });
        }

        private void Current_FileUploadError(object sender, FileUploadResponse e)
        {
            ViewModel.IsBusy = false;
            System.Diagnostics.Debug.WriteLine($"{e.StatusCode} - {e.Message}");
            Device.BeginInvokeOnMainThread(async () =>
            {
                await DisplayAlert("File Upload", "Upload Failed", "Ok");
                ProgressBar.IsVisible = false;
                ProgressBar.Progress = 0.0f;
            });
        }

        private void Current_FileUploadCompleted(object sender, FileUploadResponse e)
        {
            ViewModel.IsBusy = false;
            System.Diagnostics.Debug.WriteLine($"{e.StatusCode} - {e.Message}");
            Device.BeginInvokeOnMainThread(async () =>
            {
                await DisplayAlert("File Upload", "Upload Completed: \n " + e.Message, "Ok");
                saveUser(e.Message);

                ProgressBar.IsVisible = false;
                ProgressBar.Progress = 0.0f;

            });
        }

        async void saveUser(string photoProfileId)
        {
            User user = new User()
            {
                Name = EntryName.Text,
                Email = EntryEmail.Text,
                Password = EntryPassword.Text,
                City = PickerCity.SelectedItem as String,
                PhotoProfileId = photoProfileId
            };

            string result = await ViewModel.UserStore.AddUserAsync(user);
            await DisplayAlert("result", result, "OK");
        }
    }
}