using System;
using System.Linq;
using ReadBook.Models;
using ReadBook.ViewModels;

using Xamarin.Forms;

namespace ReadBook.Views
{
	public partial class RankingPage : ContentPage
	{
		RankingViewModel viewModel;

		public RankingPage()
		{
			InitializeComponent();

			BindingContext = viewModel = new RankingViewModel();
		}

		protected async override void OnAppearing()
		{
			base.OnAppearing();
            
            if (viewModel.Gamifications.Count == 0)
            {
                await viewModel.GetAllAsync(false);
                viewModel.LoadItemsCommand.Execute(null);
            }				
		}
	}
}
