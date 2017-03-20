using System.Linq;
using System.Threading.Tasks;
using ReadBook.Models;
using Xamarin.Forms;

namespace ReadBook.ViewModels
{
    public class SignUpViewModel : BaseViewModel
    {	  
		public SignUpViewModel()
        {
			Title = "Novo usuário";		
        }

		public async Task<bool> SignUp(User user)
		{
			await DataStore.InsertAsync(user);
			App.User = user;
			return (user != null);
		}
    }
}