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
    public partial class MenuPage : ContentPage
    {
        public MenuPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            FrameViewProfile.GestureRecognizers.Add(new TapGestureRecognizer() {
                Command = new Command(() => {
                    //FrameViewProfile.BackgroundColor = Color.WhiteSmoke;
                    DisplayAlert("Alert", "You have been alerted", "OK");
                    //FrameViewProfile.BackgroundColor = Color.Default;
                })
            });

            FrameCreateEvent.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() => {
                    Navigation.PushAsync(new CreateEventPage());
                })
            });
        }
    }
}
