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
    public partial class EventDetailPage : ContentPage
    {
        EventDetailViewModel ViewModel;

        // Note - The Xamarin.Forms Previewer requires a default, parameterless constructor to render a page.
        public EventDetailPage() {
            InitializeComponent();

            Event _event = new Event() {
                Title = "Title",
                Text = "Description",
                StartDate = DateTime.Today
            };

            BindingContext = this.ViewModel = new EventDetailViewModel(_event);
        }

        public EventDetailPage(EventDetailViewModel viewModel) {
            InitializeComponent();

            BindingContext = this.ViewModel = viewModel;
        }
    }
}