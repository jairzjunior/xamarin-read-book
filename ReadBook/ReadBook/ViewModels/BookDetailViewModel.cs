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

			IsRead();
        }

		private void IsRead()
		{           
            var userBooks = DataStore.Connection.Table<UserBook>().ToList();
            ReadText = "Marcar como lido";
			var userBook = userBooks.FirstOrDefault(u => (u.UserId == App.User.Id) & (u.BookId == Book.Id));
			Book.IsRead = (userBook != null);

			if (Book.IsRead)
			{
				ReadText = "Livro lido";
			};
		}

		public async Task ReadBook(UserBook userBook)
		{
			if (!Book.IsRead)
			{				
				var isNewUser = true;
				Gamification gamification = new Gamification();                
                var gamifications = DataStore.Connection.Table<Gamification>().ToList();
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
                foreach (var item in DataStore.Connection.Table<UserBook>().ToList())
				{
					if (item.UserId == userBook.UserId)
					{                        
                        item.Book = DataStore.Connection.Table<Book>().FirstOrDefault(b => b.Id == item.BookId);
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
					var trophy = string.Format("Leitor de {0} com {1} livros lidos \n", Book.Genre, readGenres);
					gamification.Trophy += trophy;

					MessagingCenter.Send(new MessagingCenterAlert
					{
						Title = "Premiação",
						Message = trophy,
						Cancel = "OK"
					}, "message");
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