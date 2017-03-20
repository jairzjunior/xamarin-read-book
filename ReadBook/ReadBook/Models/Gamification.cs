using Newtonsoft.Json;
using SQLite;

namespace ReadBook.Models
{
    public class Gamification : BaseDataObject
    {		
		[JsonProperty("userId")]
		public string UserId { get; set; }

		[JsonIgnore]
        [Ignore]
		public User User { get; set; }

		[JsonProperty("booksRead")]
		public int BooksRead { get; set; }

		[JsonProperty("points")]
		public int Points { get; set; }

		[JsonProperty("trophy")]
		public string Trophy { get; set; }

		public Gamification()
		{
			UserId = "";
			BooksRead = 0;
			Points = 0;
			Trophy = "";
		}
    }
}
