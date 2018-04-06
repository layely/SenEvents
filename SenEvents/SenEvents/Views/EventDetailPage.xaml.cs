using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Maps;

namespace SenEvents
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EventDetailPage : ContentPage
    {
        EventDetailViewModel ViewModel;

        // Note - The Xamarin.Forms Previewer requires a default, parameterless constructor to render a page.
        public EventDetailPage()
        {
            InitializeComponent();

            Event _event = new Event()
            {
                Title = "Title",
                Text = "Description",
                StartDate = DateTime.Today
            };

            BindingContext = this.ViewModel = new EventDetailViewModel(_event);
        }

        public EventDetailPage(EventDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.ViewModel = viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await this.ViewModel.InitAsync();
            ButtonAttend.Text = this.ViewModel.ButtonAttendText;
            startAnimation();
            MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(37, -122), Distance.FromMiles(1)));
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            //this.stopAnimation();
            this.MainParallax.DestroyParallaxView();
        }

        async void startAnimation()
        {
            uint duration = 2 * (60 * 1000); // Two minutes

            await this.Image?.RotateTo(360, 500); // this will make the app the crash when back button is hit

            while (true)
            {
                await this.Image?.ScaleTo(2, duration, Easing.BounceIn);
                await this.Image?.ScaleTo(1, duration);
            }
        }

        void stopAnimation()
        {
            ViewExtensions.CancelAnimations(this.Image);
        }

        void ButtonAttend_Clicked(object sender, EventArgs e)
        {
            //await DateTime
        }
    }
}