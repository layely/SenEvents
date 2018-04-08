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
            //await Navigation.PushAsync(new MenuPage());

            //TEsting my damned stuff here
            //User user = await viewModel.UserStore.GetUserAsync("ablayelyfondou@gmail.com");

            //string output = JsonConvert.SerializeObject(user);
            //await DisplayAlert("Serialisation test", output, "OK");


            User user = new User()
            {
                Email = "aoly@g.com",
                Name = "AOL",
                City = "THIES",
                Password = "password"
            };

            string output = await viewModel.UserStore.AddUserAsync(user);

            await Navigation.PushAsync(new TabbedLoginPage());
        }
    }
}