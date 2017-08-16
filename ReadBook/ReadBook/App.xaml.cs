using ReadBook.Models;
using ReadBook.ViewModels;
using ReadBook.Views;
using ReadBook.Helpers;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ReadBook.Interfaces;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace ReadBook
{
    public partial class App : Application
    {				
        public App()
        {
            InitializeComponent();
            
            Current.MainPage = new LoginPage();
        }

        public static void SetMainPage()
        {
            Current.MainPage = new TabbedPage
            {
                Children =
                {
                    new NavigationPage(new BooksPage())
                    {
                        Title = "Livros",
                        Icon = Device.OnPlatform("tab_feed.png",null,null)
                    },
                    new NavigationPage(new RankingPage())
                    {
                        Title = "Ranking",
                        Icon = Device.OnPlatform("tab_about.png",null,null)
                    },
                }
            };            
        }
    }
}
