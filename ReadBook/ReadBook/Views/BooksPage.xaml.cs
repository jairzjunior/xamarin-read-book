using System;

using ReadBook.Models;
using ReadBook.ViewModels;

using Xamarin.Forms;

namespace ReadBook.Views
{
    public partial class BooksPage : ContentPage
    {
        BooksViewModel viewModel;

        public BooksPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new BooksViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Book;
            if (item == null)
                return;

            await Navigation.PushAsync(new BookDetailPage(new BookDetailViewModel(item)));

            // Manually deselect item
            ItemsListView.SelectedItem = null;
        }

		async void Profile_Clicked(object sender, EventArgs e)
        {
			await Navigation.PushAsync(new ProfilePage());
        }        

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Books.Count == 0)
            {
                await viewModel.GetAllAsync(false);                
                viewModel.LoadItemsCommand.Execute(null);
            }
        }
    }
}
