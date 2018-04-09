using System;

using Xamarin.Forms;

namespace SenEvents
{
    public class MainPage : TabbedPage
    {
        Page listEventsPage = null;
        Page listEventInUserCityPage = null;

        bool IsInitialised = false;

        BaseViewModel baseViewModel;

        public MainPage()
        {
            //Page itemsPage, aboutPage = null;

            baseViewModel = new BaseViewModel();

            Title = "SenEvents";
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            InitAndAddPage();

            //Title = CurrentPage?.Title ?? string.Empty;
        }

        protected async override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();
        }

        async void InitAndAddPage()
        {
            if (this.IsInitialised)
                return;

            listEventsPage = new ListEventsPage()
            {
                Title = "Explorer"
            };

            listEventInUserCityPage = new ListEventsPage(new ListEventsViewModel(await baseViewModel.UserStore.GetCurrentUserCityAsync()))
            {
                Title = "Dans Ma Ville"
            };

            Children.Add(listEventInUserCityPage);
            Children.Add(listEventsPage);

            this.IsInitialised = true;
        }
    }
}
