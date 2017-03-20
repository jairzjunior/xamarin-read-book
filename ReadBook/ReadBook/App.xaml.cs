using ReadBook.Models;
using ReadBook.ViewModels;
using ReadBook.Views;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace ReadBook
{
    public partial class App : Application
    {
		public static bool IsUserLoggedIn { get; set; }
		public static User User { get; set; }

        public App()
        {
            InitializeComponent();

			if (!IsUserLoggedIn)
			{
				Current.MainPage = new NavigationPage(new LoginPage(new LoginViewModel()));
			}
			else
			{
				SetMainPage();
			}			            
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
