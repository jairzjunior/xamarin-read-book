using System;
using System.Diagnostics;
using System.Threading.Tasks;

using ReadBook.Helpers;
using ReadBook.Models;
using ReadBook.Views;

using Xamarin.Forms;
using System.Linq;

namespace ReadBook.ViewModels
{
    public class BooksViewModel : BaseViewModel
    {
        public ObservableRangeCollection<Book> Books { get; set; }
        public Command LoadItemsCommand { get; set; }

        public BooksViewModel()
        {
            Title = "Esse eu já li!";
            Books = new ObservableRangeCollection<Book>();
            LoadItemsCommand = new Command(() => ExecuteLoadItemsCommand());            
        }

        private void PopulateBooks()
        {
            for (int i = 1; i <= 100; i++)
            {
                string[] generos = {
                    "Ficção Científica",
                    "Romance",
                    "Tecnologia",
                    "Fantasia",
                    "Humor"
                };
                var genero = generos[new Random().Next(0, generos.Length)];

                var pages = new Random().Next(0, 500);
                var book = new Book()
                {
                    Title = string.Format("Livro {0}", i),
                    Genre = genero,
                    Pages = pages,
                    Description =
                        "Lorem ipsum dolor sit amet, " +
                        "consectetur adipiscing elit.Quisque tristique nisi ligula, " +
                        "ut ultrices tortor gravida vitae.Sed finibus ipsum urna, " +
                        "ac fringilla ligula euismod et.Nullam lobortis iaculis ex.Maecenas maximus, " +
                        "ligula consectetur dignissim viverra, " +
                        "enim mauris ultricies ligula, " +
                        "ut mattis felis nibh et quam. " +
                        "Quisque lacinia dui nec varius tempus. " +
                        "Aliquam erat volutpat.Praesent facilisis urna id neque ullamcorper mollis. " +
                        "Morbi auctor ipsum non tortor malesuada interdum.Integer non tellus sed nulla faucibus blandit. " +
                        "Nulla convallis mi lacinia sapien iaculis ullamcorper.Nunc facilisis purus eget orci aliquet," +
                        "aliquam congue sem fringilla.Interdum et malesuada fames ac ante ipsum primis in faucibus.Etiam laoreet justo tincidunt," +
                        "placerat libero sit amet," +
                        "gravida nibh.Integer eu posuere leo."
                };
                DataStore.InsertAsync(book);
            }
        }

        async void ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Books.Clear();
                var items = await DataStore.Connection.Table<Book>().ToListAsync();
                if (items.Count == 0)
                {
                    PopulateBooks();
                    items = await DataStore.Connection.Table<Book>().ToListAsync();
                }                    
                Books.ReplaceRange(items);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessagingCenter.Send(new MessagingCenterAlert
                {
                    Title = "Error",
                    Message = "Unable to load items.",
                    Cancel = "OK"
                }, "message");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}