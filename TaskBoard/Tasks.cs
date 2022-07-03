using System.Text.Json.Serialization;

namespace RestSharpAPI_Tests
{
    public class Tasks
    {
        [JsonPropertyName("id")]
        public int id { get; set; }

        [JsonPropertyName("title")]
        public string title { get; set; }

        [JsonPropertyName("description")]
        public string description { get; set; }

        [JsonPropertyName("dateCreated")]
        public string dateCreated { get; set; }

        [JsonPropertyName("dateModified")]
        public string dateModified { get; set; }
    }
    public class TaskBoards
    {
        [JsonPropertyName("id")]
        public int id { get; set; }

        [JsonPropertyName("name")]
        public string name { get; set; }
    }



}

