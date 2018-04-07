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

        public ListEventsPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new ListEventsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Events.Count == 0)
                viewModel.LoadEventsCommand.Execute(null);
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
            await Navigation.PushAsync(new TabbedLoginPage());
        }
    }
}