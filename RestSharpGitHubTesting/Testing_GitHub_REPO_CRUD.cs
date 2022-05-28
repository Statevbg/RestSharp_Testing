using System.Net;
using System;
using System.Threading.Tasks;
using NUnit.Framework;
using RestSharp;
using RestSharp.Authenticators;
using System.Text.Json;
using System.Collections.Generic;

namespace RestSharpGitHubCreatingRepoTesting
{
    public class Testing_GitHub_REPO_CRUD
    {
        private RestClient client;
        private RestRequest request;
        

        [SetUp]
        public void Setup()
        {
            this.client = new RestClient("https://api.github.com");
            this.client.Authenticator = new HttpBasicAuthenticator("TYPE USER NAME HERE", "TYPE GITHUB TOKEN(must have crud operations rights)");
            
        }
        [Test]
        public async Task RestSharp01_CreateRepository()
        {
            //Arrange
            this.request = new RestRequest("/user/repos");
            this.request.AddJsonBody(new { name = "ThisRepoIsCreatedByRestSharp" });
            this.request.AddHeader("Accept", "application/vnd.github.v3+json");
            //Act
            var response = await this.client.ExecuteAsync(this.request, Method.Post);
            //Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        }
        [Test]
        public async Task RestSharp02_GetRepository()
        {
            //Arrange
            this.request = new RestRequest("repos/TYPE USER NAME HERE/ThisRepoIsCreatedByRestSharp");
            this.request.AddHeader("Accept", "application/vnd.github.v3+json");
            //Act
            var response = await this.client.ExecuteAsync(this.request, Method.Get);
            //Assert
            Console.WriteLine(response.Content);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
        [Test]
        public async Task RestSharp03_UpdateRepository()
        {
            //Arrange
            this.request = new RestRequest("repos/TYPE USER NAME HERE/ThisRepoIsCreatedByRestSharp");
            this.request.AddJsonBody(new { name = "ThisRepoIsUpdatedByRestSharp" });
            this.request.AddHeader("Accept", "application/vnd.github.v3+json");
            //Act
            var response = await this.client.ExecuteAsync(this.request, Method.Patch);
            //Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
        [Test]
        public async Task RestSharp04_DeleteRepository()
        {
            //Arrange
            this.request = new RestRequest("repos/TYPE USER NAME HERE/ThisRepoIsUpdatedByRestSharp");
            this.request.AddHeader("Accept", "application/vnd.github.v3+json");
            //Act
            var response = await this.client.ExecuteAsync(this.request, Method.Delete);
            //Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
        }
    }
}
