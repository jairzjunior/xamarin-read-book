using ReadBook.ViewModels;
using Xamarin.Forms;

namespace ReadBook.Views
{
	public partial class ProfilePage : ContentPage
    {		
		ProfileViewModel viewModel;
		public ProfilePage()
		{
			viewModel = new ProfileViewModel();

			InitializeComponent();

			BindingContext = this.viewModel;
		}
    }
}