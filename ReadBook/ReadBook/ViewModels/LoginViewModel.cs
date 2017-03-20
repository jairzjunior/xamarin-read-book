using System.Linq;
using System.Threading.Tasks;
using ReadBook.Models;

namespace ReadBook.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {		
		public bool Login(User user)
		{
			App.User = null;            
            var users = DataStore.Connection.Table<User>().ToList();
            if (users != null && users.Count > 0)

            {
				var userAzure = users.First(u => u.Username == user.Username);
				if ((userAzure != null) && (user.Password == userAzure.Password))
				{
					App.User = userAzure;
				}
			}
			return App.User != null;
		}
    }
}