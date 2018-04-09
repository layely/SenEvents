using Newtonsoft.Json;
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
    public partial class ListEventsPage : ContentPage
    {
        ListEventsViewModel viewModel;
        bool NeedUpdate = true;

        public ListEventsPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new ListEventsViewModel();
        }

        public ListEventsPage(ListEventsViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = this.viewModel = viewModel;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            if (this.NeedUpdate && viewModel.Events.Count == 0)
            {
                viewModel.LoadEventsCommand.Execute(null);
                this.NeedUpdate = false;
            }
        }

        async void OnEventSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var _event = args.SelectedItem as Event;
            if (_event == null)
                return;

            await Navigation.PushAsync(new EventDetailPage(new EventDetailViewModel(_event)));

            // Manually deselect item
            this.EventsListView.SelectedItem = null;
        }

        async void Compte_clicked(object sender, EventArgs e)
        {
            if (viewModel.IsBusy)
                return;
            viewModel.IsBusy = true;

            if (await viewModel.UserStore.IsAUserConnectedAsync())
            {
                await Navigation.PushAsync(new MenuPage());
            }
            else
            {
                await Navigation.PushAsync(new TabbedLoginPage());
            }

            viewModel.IsBusy = false;
        }
    }
}