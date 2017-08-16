using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using ReadBook.Interfaces;
using ReadBook.Droid.Implementations;
using Xamarin.Forms;
using System;

[assembly: Dependency(typeof(SocialAuthentication))]
namespace ReadBook.Droid.Implementations
{
    public class SocialAuthentication : IAuthentication
    {        
        public async Task<MobileServiceUser> LoginAsync(MobileServiceClient client, MobileServiceAuthenticationProvider provider, IDictionary<string, string> parameters = null)
        {            
            try
            {
                var user = await client.LoginAsync(Forms.Context, provider);
                return user;
            }
            catch (Exception ex)
            {
                //TODO: Log error
                throw;
            }
        }
    }
}