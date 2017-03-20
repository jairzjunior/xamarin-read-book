using SQLite;

namespace ReadBook.Interfaces
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}