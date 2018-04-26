using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Media;
using Plugin.FileUploader.Abstractions;

namespace SenEvents
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateEventPage : ContentPage
    {
        CreateEventViewModel ViewModel;

        public CreateEventPage()
        {
            InitializeComponent();

            this.BindingContext = ViewModel = new CreateEventViewModel();

        }

        protected override void OnAppearing()
        {
            DatePickerStart.MinimumDate = DateTime.Now;
            DatePickerStart.Date = DateTime.Now;

            PickerCity.ItemsSource = ViewModel.CityList;
            PickerCategory.ItemsSource = ViewModel.CategoryList;
        }

        async void ButtonGallery_Clicked(object sender, EventArgs e)
        {
            GetImage();
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
                this.Image.Source = pickedImage.ImageSource;
                ViewModel.pickedImagePath = pickedImage.Path;
            }

            ViewModel.IsBusy = false;
        }

        async void ButtonCreerEvenement_Clicked(object sender, EventArgs e)
        {
            if (ViewModel.IsBusy)
                return;
            ViewModel.IsBusy = true;

            string title = EntryTitle.Text,
                place = EntryPlace.Text,
                city = PickerCity.SelectedItem as string,
                category = PickerCategory.SelectedItem as string,
                organizer = EntryOrganizer.Text,
                description = EditorDescription.Text;

            DateTime datestart = DateUtil.GetDateTimeFromPickers(DatePickerStart.Date, TimePickerStart.Time);

            int price = 0;
            int.TryParse(EntryPrice.Text, out price);

            await DisplayAlert("pff", "date: " + datestart.ToString(), "OK");

            ProgressBar.Progress = 0;
            ProgressBar.IsVisible = true;

            // Note that here the upload result will manage to reset the busy-state to false once complete.
            ViewModel.ImageStore.upload(ViewModel.pickedImagePath, Current_FileUploadCompleted, Current_FileUploadError, Current_FileUploadProgress);
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
                string photoUri = e.Message;
                SaveEvent(photoUri);

                ProgressBar.IsVisible = false;
                ProgressBar.Progress = 0.0f;

            });

        }

        private async void SaveEvent(string photoUri)
        {
            string title = EntryTitle.Text,
                place = EntryPlace.Text,
                city = PickerCity.SelectedItem as string,
                category = PickerCategory.SelectedItem as string,
                organizer = EntryOrganizer.Text,
                description = EditorDescription.Text;

            int price = int.Parse(EntryPrice.Text);

            DateTime dateStart = DatePickerStart.Date;

            Event _event = new Event
            {
                Title = title,
                PhotoUri = photoUri,
                Text = description,
                Price = price,
                StartDate = dateStart,
                EndDate = dateStart,
                Categories = category,
                Place = place,
                City = city,
                Organization = organizer,
                PublisherEmail = await ViewModel.UserStore.GetCurrentUserEmailAsync()
            };

            await DisplayAlert("Result", await ViewModel.EventStore.AddItemAsync(_event), "OK");
        }
    }
}