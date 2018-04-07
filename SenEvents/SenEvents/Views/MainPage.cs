using System;

using Xamarin.Forms;

namespace SenEvents
{
    public class MainPage : TabbedPage
    {
        public MainPage()
        {
            //Page itemsPage, aboutPage = null;
            Page listEventsPage = null;
            Page listEventInUserCityPage = null;

            BaseViewModel baseViewModel = new BaseViewModel();

            listEventsPage = new ListEventsPage()
            {
                Title = "Explorer"
            };

            listEventInUserCityPage = new ListEventsPage(new ListEventsViewModel(baseViewModel.UserStore.GetCurrentUserCity()))
            {
                Title = "Dans Ma Ville"
            };

            Children.Add(listEventInUserCityPage);
            Children.Add(listEventsPage);

            Title = "SenEvents";
        }

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();
            //Title = CurrentPage?.Title ?? string.Empty;
        }
    }
}
