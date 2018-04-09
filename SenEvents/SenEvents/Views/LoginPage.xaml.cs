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
    public partial class LoginPage : ContentPage
    {
        LoginViewModel viewModel;

        public LoginPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new LoginViewModel();
        }

        async void ButtonLogin_clicked(object sender, EventArgs e)
        {
            if (viewModel.IsBusy)
                return;
            viewModel.IsBusy = true;

            string email = EntryEmail.Text;
            string password = EntryPassword.Text;

            User user = await viewModel.UserStore.GetUserAsync(email);
            if (user == null)
            {
                await DisplayAlert("Erreur", "Cet utilisateur n'existe pas", "Ok");
            }
            else if (!user.Password.Equals(password))
            {
                await DisplayAlert("Erreur", "Mot de passe incorrect", "Ok");
            }
            else {
                await viewModel.UserStore.SaveConnetedUserAsync(user);
                await Navigation.PushAsync(new MenuPage());

                MessagingCenter.Send<Page>(this, "die");
            }

            viewModel.IsBusy = false;
        }
    }
}