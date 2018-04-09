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
        public MenuViewModel viewModel;
        public MenuPage()
        {
            InitializeComponent();
            BindingContext = this.viewModel = new MenuViewModel();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            FrameViewProfile.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(async () =>
                {
                    if (viewModel.IsBusy)
                        return;
                    viewModel.IsBusy = true;

                    await DisplayAlert("Alert", "You have been alerted", "OK");

                    viewModel.IsBusy = false;
                })
            });

            FrameCreateEvent.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(async () =>
                {
                    if (viewModel.IsBusy)
                        return;
                    viewModel.IsBusy = true;

                    await Navigation.PushAsync(new CreateEventPage());

                    viewModel.IsBusy = false;
                })
            });

            FrameLogout.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(async () =>
                {
                    if (viewModel.IsBusy)
                        return;
                    viewModel.IsBusy = true;

                    bool confirm = await DisplayAlert("Déconnexion", "Voulez-vous vraiment vous déconnecter?", "Oui", "Non");
                    if (confirm)
                    {
                        viewModel.UserStore.DeleteConnectedUserAsync();
                        Navigation.RemovePage(this);
                    }

                    viewModel.IsBusy = false;

                })
            });
        }
    }
}
