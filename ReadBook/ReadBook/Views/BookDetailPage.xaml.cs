
using System;
using ReadBook.Models;
using ReadBook.ViewModels;
using ReadBook.Helpers;

using Xamarin.Forms;

namespace ReadBook.Views
{
    public partial class BookDetailPage : ContentPage
    {
        BookDetailViewModel viewModel;

        // Note - The Xamarin.Forms Previewer requires a default, parameterless constructor to render a page.
        public BookDetailPage()
        {
            InitializeComponent();
        }

        public BookDetailPage(BookDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

		async void Read_Clicked(object sender, EventArgs e)
		{
			var userBook = new UserBook();
			userBook.BookId = viewModel.Book.Id;
			userBook.UserId = Settings.UserId;

			await Navigation.PopToRootAsync();
			await viewModel.ReadBookAsync(userBook);
		}

        protected async override void OnAppearing()
        {            
            base.OnAppearing();
            await viewModel.IsReadAsync(false);
            await viewModel.IsReadAsync(true);
        }
    }
}
