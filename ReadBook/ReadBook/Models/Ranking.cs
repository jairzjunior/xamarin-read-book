using Newtonsoft.Json;

namespace ReadBook.Models
{
    public class Ranking : BaseDataObject
    {				
		[JsonProperty("userId")]
		public string UserId { get; set; }

		[JsonProperty("readBook")]
		public int ReadBook { get; set; }

		[JsonProperty("score")]
		public int Score { get; set; }

		public Ranking()
		{			
			UserId = "";
			ReadBook = 0;
			Score = 0;
		}
    }
}
