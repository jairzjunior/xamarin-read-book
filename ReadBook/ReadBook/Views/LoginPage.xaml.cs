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

			BindingContext = this.viewModel = new LoginViewModel();
		}		
	}
}