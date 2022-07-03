using NUnit.Framework;
using RestSharp;
using RestSharpAPI_Tests;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Exam_AutomationQA_SoftUni
{
    public class APITests
    {
        private const string url = "https://taskboard.dimitarstatev.repl.co/";
        private RestClient client;
        private RestRequest request;

        [SetUp]
        public void Setup()
        {
            this.client = new RestClient();
        }

        [Test]
        public void Test1_Get_List_AllTasks_CheckFirstResult()
        {
            //Arrange
            this.request = new RestRequest(url +"api/tasks");
             //Act
            var response = this.client.Execute(request, Method.Get);
            var tasks = JsonSerializer.Deserialize<List<Tasks>>(response.Content);
            //Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(tasks[0].title, Is.EqualTo("Project skeleton"));
            Assert.That(tasks[0].description, Is.EqualTo("Create project folders, services, controllers and views"));
        }

        [Test]
        public void Test2_Search_Task_For_Keyword_Home()
        {
            //Arrange
            this.request = new RestRequest(url + "api/tasks/search/{keyword}");
            request.AddUrlSegment("keyword", "home");
            //Act
            var response = this.client.Execute(request, Method.Get);
            var tasks = JsonSerializer.Deserialize<List<Tasks>>(response.Content);
            //Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(tasks[0].title, Is.EqualTo("Home page"));
         
        }
        [Test]
        public void Test3_Search_Task_Invalid_Keyword()
        {
            //Arrange
            this.request = new RestRequest(url + "api/tasks/search/{keyword}");
            request.AddUrlSegment("keyword", "missing{6}");
            //Act
            var response = this.client.Execute(request, Method.Get);
            var tasks = JsonSerializer.Deserialize<List<Tasks>>(response.Content);
            //Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(tasks.Count, Is.EqualTo(0));
        }
                 
        [Test]
        public void Test4_Create_NewTask_ValidData()
        {
            //Arrange
            this.request = new RestRequest(url + "api/tasks/");

            var body = new
            {
                title = "Add Tests",
                description = "API + UI tests",
                board = "Open"
            };

            request.AddJsonBody(body);
            //Act
            var response = this.client.Execute(request, Method.Post);
          
            var allTasks = this.client.Execute(request, Method.Get);
            var tasks = JsonSerializer.Deserialize<List<Tasks>>(allTasks.Content);
            var lastTask = tasks.Last();

            //Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(lastTask.title, Is.EqualTo("Add Tests"));







        }
        [Test]
        public void Test5_Create_NewTask_InvalidData()
        {
            //Arrange
            this.request = new RestRequest(url + "api/tasks/");

            var body = new
            {
               description = "Test",
               board = "Open"
            };
            request.AddJsonBody(body);
            //Act
            var response = this.client.Execute(request, Method.Post);
            //Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            Assert.That(response.Content, Is.EqualTo("{\"errMsg\":\"Title cannot be empty!\"}"));

        }
    }
}