using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using RestSharp.Serializers.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Text.Json.Serialization;

namespace TestZipopotamusApi
{ 
    public class Location 
    {
        [JsonPropertyName("post code")]
        public string postCode { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }
        
        [JsonPropertyName("country abbreviation")]
        public string CountryAbbreviation { get; set; }

        [JsonPropertyName("places")]
        public List<Place> Places { get; set; }

    }
    public class Place
    {
        [JsonPropertyName("place name")]

        public string PlaceName { get; set; }
        public string State { get; set; }
        public string StateAbbreviation { get; set; }
        public string Latitude  { get; set; }
        public string Longitude { get; set; }


    }
    public class ZipopotamusApiTests
    {
        [TestCase("BG", "1000", "София / Sofija")]
        [TestCase("BG", "8600", "Ямбол / Jambol")]
        [TestCase("BG", "8000", "Бургас / Burgas")]
        [TestCase("BG", "8400", "Карнобат / Karnobat")]
        [TestCase("CA", "M5S", "Downtown Toronto (University of Toronto / Harbord)")]
        [TestCase("GB", "B1", "Birmingham")]
        [TestCase("DE", "01067", "Dresden")]
        public async Task TestZipopotamus(string countryCode, string zipCode, string expectedPlace)
        {
            //Arange
            RestClient client = new RestClient("http://api.zippopotam.us/");
            RestRequest request = new RestRequest(countryCode + "/" + zipCode, Method.Get);
            //Act
            var response = await client.GetAsync(request);
            var location = new SystemTextJsonSerializer().Deserialize<Location>(response);
            //Assert
            Assert.That(location.Places[0].PlaceName, Is.EqualTo(expectedPlace));

        }
    }
}
