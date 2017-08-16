using System.Threading.Tasks;
using ReadBook.Models;

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
			return (user != null);
		}
    }
}