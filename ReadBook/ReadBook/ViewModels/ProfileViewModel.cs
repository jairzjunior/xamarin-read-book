using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReadBook.Models;
using ReadBook.Helpers;

namespace ReadBook.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {
        private User user = null;
        public User User
        {
            get { return user; }
            set { SetProperty(ref user, value); }
        }

        private Gamification gamification = null;
		public Gamification Gamification
		{
			get { return gamification; }
			set { SetProperty(ref gamification, value); }
		}

        private string imageProfile;

        public string ImageProfile
        {
            get { return imageProfile; }
            set { SetProperty(ref imageProfile, value); }
        }

        public ProfileViewModel()
        {
			Title = "Perfil";            
        }

        public async Task LoadUserAsync()
        {
            await DataStore.SyncAsync<User>(true);
            User = (await DataStore.GetAllAsync<User>()).FirstOrDefault(u => u.Id == Settings.UserId);
            ImageProfile = $"http://graph.facebook.com/{user.NameIdentifier}/picture?type=large";
        }

        public async Task LoadGamificationAsync()
		{
            await DataStore.SyncAsync<Gamification>(true);
            var gamifications = await DataStore.GetAllAsync<Gamification>();
            if (gamifications != null)
			{
				Gamification = gamifications.FirstOrDefault(a => a.UserId == User?.Id);
			}
		}
    }
}