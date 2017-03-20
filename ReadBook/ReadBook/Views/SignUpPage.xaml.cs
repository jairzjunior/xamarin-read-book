using System;
using System.Linq;
using System.Threading.Tasks;
using ReadBook.Models;
using ReadBook.ViewModels;
using Xamarin.Forms;

namespace ReadBook.Views
{
	public partial class SignUpPage : ContentPage
	{
		SignUpViewModel viewModel;

		public SignUpPage()
		{
			InitializeComponent();
		}

		public SignUpPage(SignUpViewModel viewModel)
		{
			InitializeComponent();

			BindingContext = this.viewModel = viewModel;
		}

		async void OnSignUpButtonClicked(object sender, EventArgs e)
		{
			var user = new User()
			{
				FirstName = fistNameEntry.Text,
				LastName = lastNameEntry.Text,
				Username = usernameEntry.Text,
				Password = passwordEntry.Text,
				Email = emailEntry.Text
			};

			// Sign up logic goes here

			var signUpSucceeded = await AreDetailsValid(user);
			if (signUpSucceeded)
			{				
				var rootPage = Navigation.NavigationStack.FirstOrDefault();
				if (rootPage != null)
				{					
					App.IsUserLoggedIn = true;
					App.SetMainPage();
					await Navigation.PopToRootAsync();
				}
			}
			else
			{
				messageLabel.Text = "Falha ao efetuar cadastro";
			}
		}

		async Task<bool> AreDetailsValid(User user)
		{
			//return (!string.IsNullOrWhiteSpace(user.Username) && !string.IsNullOrWhiteSpace(user.Password) && !string.IsNullOrWhiteSpace(user.Email) && user.Email.Contains("@"));
			return await viewModel.SignUp(user);
		}
	}
}