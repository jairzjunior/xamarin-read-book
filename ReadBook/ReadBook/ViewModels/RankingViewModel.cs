using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ReadBook.Helpers;
using ReadBook.Models;
using Xamarin.Forms;

namespace ReadBook.ViewModels
{
    public class RankingViewModel : BaseViewModel
    {		
		public ObservableRangeCollection<Gamification> Gamifications { get; set; }
		public Command LoadItemsCommand { get; set; }

        public RankingViewModel()
        {
            Title = "Ranking";
			Gamifications = new ObservableRangeCollection<Gamification>();
			LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }

		async Task ExecuteLoadItemsCommand()
		{
			if (IsBusy)
				return;

			IsBusy = true;

			try
			{
				Gamifications.Clear();                
                var items = await DataStore.Connection.Table<Gamification>().ToListAsync();
                foreach (var item in items.OrderByDescending(g => g.Points))
				{                    
                    item.User = (await DataStore.Connection.Table<User>().ToListAsync()).FirstOrDefault(u => u.Id == item.UserId);
                    Gamifications.Add(item);
				}
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
