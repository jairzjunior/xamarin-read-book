using SQLite;
using System.IO;
using Xamarin.Forms;
using ReadBook.Interfaces;
using ReadBook.iOS.Storage.Implementations;

[assembly: Dependency(typeof(SQLiteIOS))]
namespace ReadBook.iOS.Storage.Implementations
{
    public class SQLiteIOS : ISQLite
    {
        public SQLiteConnection GetConnection()
        {
            SQLitePCL.Batteries.Init();
            var sqliteFilename = "db.db3";
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.UserProfile);
            var path = Path.Combine(documentsPath, sqliteFilename);

            var conn = new SQLiteConnection(path);

            return conn;
        }
    }
}