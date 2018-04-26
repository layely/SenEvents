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

            // Title = CurrentPage?.Title ?? string.Empty;
            autoLogin();
        }

        private async void autoLogin()
        {
            // Let's connect automatically a user (for testing purpose)
            IUserStore userStore = new MockUserStore();
            if (!await userStore.IsAUserConnectedAsync())
            {
                User ablayelyfondou = await userStore.GetUserAsync("ablayelyfondou@gmail.com");
                await userStore.SaveConnetedUserAsync(ablayelyfondou);
            }
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
