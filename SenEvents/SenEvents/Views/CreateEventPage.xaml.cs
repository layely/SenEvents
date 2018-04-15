using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Media;

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
            if (ViewModel.IsBusy)
                return;
            ViewModel.IsBusy = true;

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Photos non supportées", ":( Permission not granted to photos.", "OK");
                return;
            }
            var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,
            });

            if (file != null)
            {
                Image.Source = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    file.Dispose();
                    return stream;
                });
            }

            ViewModel.IsBusy = false;
        }
    }
}