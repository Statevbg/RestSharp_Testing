using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using NUnit.Framework;
using RestSharp;
using RestSharp.Authenticators;

namespace RestSharpGitHubCreatingRepoTesting
{
    public class Testing_GitHub_ISSUE_CRUD

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
        public async Task RestSharp01_CreateRepositoryForIssueTesting()
        {
            //Arrange
            this.request = new RestRequest("/user/repos");
            this.request.AddJsonBody(new { name = "RepoForIssuesTesting", });
            this.request.AddHeader("Accept", "application/vnd.github.v3+json");
            //Act
            var response = await this.client.ExecuteAsync(this.request, Method.Post);
            //Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        }
        [Test]
        public async Task RestSharp02_CreatingRepositoryIssue()
        {
            //Act
            var url = "repos/TYPE USER NAME HERE/RepoForIssuesTesting/issues";
            this.request = new RestRequest(url);
            this.request.AddBody(new { title = "SharpRestTest" });
            //Arrange
            var response = await this.client.ExecuteAsync(this.request, Method.Post);
            //Assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }
        [Test]
        public async Task RestSharp03_UpdatingRepositoryIssue()
        {
            //Act
            var url = "repos/TYPE USER NAME HERE/RepoForIssuesTesting/issues/1";
            this.request = new RestRequest(url);
            this.request.AddBody(new { title = "SharpRestTest2" });
            //Arrange
            var response = await this.client.ExecuteAsync(this.request, Method.Patch);
            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
        [Test]
        public async Task RestSharp04_GetRepositoryIssues()
        {
            //Act
            var url = "repos/TYPE USER NAME HERE/RepoForIssuesTesting/issues";
            this.request = new RestRequest(url);
            //Arrange
            var response = await this.client.ExecuteAsync(this.request, Method.Get);
            var issues = JsonSerializer.Deserialize<List<Issue>>(response.Content);
            //Assert
            foreach (var issue in issues)
            {
                Console.WriteLine(issue.id);
                Assert.IsNotNull(issue.html_url);
                Assert.IsNotNull(issue.id);
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }
            
            
        }
        public class Issue
        {
            public long id { get; set; }
            public string html_url { get; set; }

        }
    }
}

