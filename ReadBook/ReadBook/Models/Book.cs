using Newtonsoft.Json;
using SQLite;

namespace ReadBook.Models
{
    public class Book : BaseDataObject
    {				
		[JsonProperty("title")]
		public string Title { get; set; }

		[JsonProperty("description")]
		public string Description { get; set; }

		[JsonProperty("genre")]
		public string Genre { get; set; }

		[JsonProperty("pages")]
		public int Pages { get; set; }

		[JsonIgnore]
        [Ignore]
        public bool IsRead { get; set; }

		public Book()
		{
			Title = "";
			Description = "";
			IsRead = false;
			Pages = 0;
		}
    }
}
