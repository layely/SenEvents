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

        void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            // Manually deselect item
            EventsListView.SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Events.Count == 0)
                viewModel.LoadEventsCommand.Execute(null);
        }
    }
}