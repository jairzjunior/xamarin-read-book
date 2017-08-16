using System.Linq;
using System.Threading.Tasks;
using ReadBook.Models;
using Xamarin.Forms;
using System.Windows.Input;
using ReadBook.Helpers;

namespace ReadBook.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {        
        public ICommand LoginCommand { get; }
        public LoginViewModel()
        {
            LoginCommand = new Command(async () => await ExecureLoginCommandAsync());            

            Title = "Login";
        }
        private async Task ExecureLoginCommandAsync()
        {
            if (IsBusy || !(await LoginAsync()))
                return;
            else
            {
                App.SetMainPage();                
            }
        }        

        public Task<bool> LoginAsync()
        {
            if (Settings.IsLoggedIn)
            {                
                return Task.FromResult(true);
            }

            return DataStore.LoginAsync();
        }        
    }
}