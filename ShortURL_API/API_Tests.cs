using RestSharp;
using System.Net;
using System.Text.Json;

namespace Exam_SeleniumWebDriverUI_Tests
{
    public class Appium_Tests
    {
        private const string url = "https://shorturl.dimitarstatev.repl.co/";
        private RestClient client;
        private RestRequest request;

        [SetUp]
        public void Setup()
        {
            this.client = new RestClient();
        }

        [Test]
        public void Get_All_ShortUrls()
        {
            // Arrange
            this.request = new RestRequest(url + "/api/urls");

            // Act
            var response = this.client.Execute(request, Method.Get);
            var urls = JsonSerializer.Deserialize<List<ShortUrls>>(response.Content);


            // Assert
            Assert.That(urls.Count, Is.GreaterThan(0));

        }
        [Test]
        public void Find_URL_ValidData()
        {
            // Arrange
            this.request = new RestRequest(url + "/api/urls/{keyword}");
            request.AddUrlSegment("keyword", "seldev");

            // Act
            var response = this.client.Execute(request, Method.Get);
            var urls = JsonSerializer.Deserialize<ShortUrls>(response.Content);


            // Assert
            Assert.That(urls.shortCode, Is.EqualTo("seldev"));
        }

        [Test]
        public void Find_URL_InvalidData()
        {
            // Arrange
            this.request = new RestRequest(url + "/api/urls/{keyword}");
            request.AddUrlSegment("keyword", "randomGuy");

            // Act
            var response = this.client.Execute(request, Method.Get);
            var urls = JsonSerializer.Deserialize<ShortUrls>(response.Content);


            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            Assert.That(urls.errMsg, Is.EqualTo("Short code not found: randomGuy"));

        }
        [Test]

        public void Create_New_ShortURL()
        {
            // Arrange
            this.request = new RestRequest(url + "api/urls/");
            var body = new
            {
                url = "https://www.investor.bg/",
                shortCode = "invest"
            };
            request.AddJsonBody(body);

            // Act
            var response = this.client.Execute(request, Method.Post);


            var newresponse = this.client.Execute(request, Method.Get);
            var urls = JsonSerializer.Deserialize<List<ShortUrls>>(newresponse.Content);


            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.AreEqual("invest", urls[urls.Count - 1].shortCode);

        }
        [Test]

        public void Delete_Newly_Created_ShortUrl()
        {
            // Arrange
            this.request = new RestRequest(url + "api/urls/");
            var body = new
            {
                url = "https://www.wikipedia.org/",
                shortCode = "wiki"
            };
            request.AddJsonBody(body);

            // Act
            var response = this.client.Execute(request, Method.Post);

            request = new RestRequest(url + "api/urls/{keyword}");
            request.AddUrlSegment("keyword", "wiki");
            var newresponse = this.client.Execute(request, Method.Delete);


            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(newresponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));


        }
    }
}