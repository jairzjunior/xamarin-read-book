using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReadBook.Helpers;
using ReadBook.Models;
using Xamarin.Forms;

namespace ReadBook.ViewModels
{
    public class BookDetailViewModel : BaseViewModel
    {		
		public Book Book { get; set; }

		string readText = string.Empty;
		public string ReadText
		{
			get { return readText; }
			set { SetProperty(ref readText, value); }
		}

        public BookDetailViewModel(Book book = null)
        {
			Title = book.Title;
            Book = book;            
        }

		public async Task IsReadAsync(bool pull)
		{
            await DataStore.SyncAsync<UserBook>(pull);
            var userBook = (await DataStore.GetAllAsync<UserBook>())?.FirstOrDefault(a => a.BookId == Book.Id);
			Book.IsRead = (userBook != null);
            ReadText = Book.IsRead ? "Livro lido" : "Marcar como lido";            
		}

		public async Task ReadBookAsync(UserBook userBook)
		{
			if (!Book.IsRead)
			{				
				var isNewUser = true;
                await DataStore.SyncAsync<Gamification>();
                Gamification gamification = new Gamification();                
                var gamifications = await DataStore.GetAllAsync<Gamification>();
                if (gamifications != null)
				{
					var gamificationUser = gamifications.FirstOrDefault(a => a.UserId == userBook.UserId);
					isNewUser = (gamificationUser == null) || (gamificationUser.UserId == null);
					if (isNewUser)
					{
						gamification = new Gamification();
					}
					else
					{
						gamification = gamificationUser;
					}
				}
				gamification.UserId = userBook.UserId;
				gamification.BooksRead++;
				int valuePoint = Book.Pages / 100;
				valuePoint++;
				gamification.Points += valuePoint;

				var userBooks = new List<UserBook>();                
                foreach (var item in await DataStore.GetAllAsync<UserBook>())
				{
					if (item.UserId == userBook.UserId)
					{                        
                        item.Book = (await DataStore.GetAllAsync<Book>())?.FirstOrDefault(b => b.Id == item.BookId);
                        userBooks.Add(item);
					}
				}
				int readGenres = 0;
				try
				{
					readGenres = userBooks.Count(b => b.Book.Genre.Equals(Book.Genre));
				}
				catch
				{
					readGenres = 0;
				}
				readGenres++;
				if (readGenres == 5)
				{
					var trophy = $"Leitor de {Book.Genre} com {readGenres} livros lidos \n";
					gamification.Trophy += trophy;

                    await App.Current.MainPage.DisplayAlert("Premiação", trophy, "Ok");                    
				}							

				if (isNewUser)
				{
					await DataStore.InsertAsync(gamification);
				}
				else
				{					
					await DataStore.UpdateAsync(gamification);
				}

				await DataStore.InsertAsync(userBook);
			}
		}

        int quantity = 1;
        public int Quantity
        {
            get { return quantity; }
            set { SetProperty(ref quantity, value); }
        }
    }
}