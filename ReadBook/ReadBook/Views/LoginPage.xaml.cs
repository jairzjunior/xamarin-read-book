using System;
using System.Threading.Tasks;
using ReadBook.Models;
using ReadBook.ViewModels;
using Xamarin.Forms;

namespace ReadBook.Views
{
	public partial class LoginPage : ContentPage
	{
		LoginViewModel viewModel;

		public LoginPage()
		{
			InitializeComponent();
		}

		public LoginPage(LoginViewModel viewModel)
		{
			InitializeComponent();

			BindingContext = this.viewModel = viewModel;
		}

		async void OnSignUpButtonClicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new SignUpPage(new SignUpViewModel()));
		}

		async void OnLoginButtonClicked(object sender, EventArgs e)
		{
			var user = new User
			{
				Username = usernameEntry.Text,
				Password = passwordEntry.Text
			};

			var isValid = await AreCredentialsCorrect(user);
			if (isValid)
			{
				App.IsUserLoggedIn = true;
				App.SetMainPage();
				await Navigation.PopAsync();
			}
			else
			{
				messageLabel.Text = "Usuário e senha não conferem!";
				passwordEntry.Text = string.Empty;
			}
		}

		async Task<bool> AreCredentialsCorrect(User user)
		{
			return await viewModel.Login(user);
		}
	}
}