using System;

using Xamarin.Forms;

namespace SenEvents
{
    public class TabbedLoginPage : TabbedPage
    {
        LoginPage loginPage;
        SignUpPage signUpPage;

        public TabbedLoginPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);

            loginPage = new LoginPage()
            {
                Title = "Se Connecter"
            };

            signUpPage = new SignUpPage()
            {
                Title = "Créer un Compte"
            };

            Children.Add(loginPage);
            Children.Add(signUpPage);

            //Title = "SenEvents";
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            MessagingCenter.Subscribe<Page>(this, "die", (sender) => {
                Navigation.RemovePage(this);
            });
        }

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();
            //Title = CurrentPage?.Title ?? string.Empty;
        }
    }
}
