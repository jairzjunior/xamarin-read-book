using Microsoft.WindowsAzure.MobileServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReadBook.Interfaces
{
    public interface IAuthentication
    {
        Task<MobileServiceUser> LoginAsync(
            MobileServiceClient client,
            MobileServiceAuthenticationProvider provider,
            IDictionary<string, string> parameters = null
        );
    }
}
