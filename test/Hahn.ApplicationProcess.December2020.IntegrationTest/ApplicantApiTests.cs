using Hahn.ApplicationProcess.December2020.Domain.Commands;
using Hahn.ApplicationProcess.December2020.Domain.Entities;
using Hahn.ApplicationProcess.December2020.IntegrationTest.TestSetup;
using Hahn.ApplicationProcess.December2020.Web;
using Newtonsoft.Json;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Hahn.ApplicationProcess.December2020.IntegrationTest
{
    [TestCaseOrderer("Hahn.ApplicationProcess.December2020.IntegrationTest.TestSetup.TestPriority", "Hahn.ApplicationProcess.December2020.IntegrationTest")]
    public class ApplicantApiTests : IClassFixture<TestingWebApplicationFactory<Startup>>
    {
        private const string ApiUrl = "/api/v1/applicants";
        private readonly HttpClient _client;

        public ApplicantApiTests(TestingWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact, TestPriority(1)]
        public async Task Get_Applicant_When_Exits_Returns_Ok()
        {
            // Arrange
            var applicantId = InitialData.Applicants.First().Id;

            // Act
            var response = await _client.GetAsync($"{ApiUrl}/{applicantId}");
            var content = await response.Content.ReadAsStringAsync();
            var applicant = JsonSerializer.Deserialize<ApplicantEntity>(content);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(applicant);
        }

        [Fact, TestPriority(1)]
        public async Task Create_Applicant_When_Country_Name_Not_Valid_Returns_BadRequest()
        {
            // Arrange
            var command = new ApplicantAddCommand("FirstName", "LastName", 26, "some@one.com", "Good address", "BadName", false);

            // Act
            var response = await _client.PostAsync(ApiUrl, new JsonContent(command));
            var content = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Contains("Country name is not valid", content);
        }

        [Fact, TestPriority(1)]
        public async Task Create_Applicant_When_Data_Is_Valid_Returns_Created()
        {
            // Arrange
            var command = new ApplicantAddCommand("FirstName", "LastName", 26, "some@one.com", "Good address", "Germany", false);

            // Act
            var response = await _client.PostAsync(ApiUrl, new JsonContent(command));
            var content = await response.Content.ReadAsStringAsync();
            var applicant = JsonConvert.DeserializeObject<ApplicantEntity>(content);

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.NotNull(applicant);
            Assert.NotNull(response.Headers.Location);
            Assert.Equal(response.Headers.Location.LocalPath, $"{ApiUrl}/{applicant.Id}");
        }

        [Fact, TestPriority(1)]
        public async Task Update_Applicant_When_Not_Exist_Returns_BadRequest()
        {
            // Arrange
            var command = new ApplicantAddCommand("FirstName", "LastName", 26, "some@one.com", "Good address", "Germany", false);
            var applicantId = 101;

            // Act
            var response = await _client.PutAsync($"{ApiUrl}/{applicantId}", new JsonContent(command));
            var content = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Contains("Applicant could not be found", content);
        }

        [Fact, TestPriority(1)]
        public async Task Update_Applicant_When_When_Data_Is_Valid_Returns_Ok()
        {
            // Arrange
            var command = new ApplicantEditCommand("FirstName", "LastName", 26, "some@one.com", "Good address", "Germany", false);
            var applicantId = InitialData.Applicants.First().Id;

            // Act
            var response = await _client.PutAsync($"{ApiUrl}/{applicantId}", new JsonContent(command));

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact, TestPriority(1)]
        public async Task Delete_Applicant_When_Not_Exist_Returns_BadRequest()
        {
            // Arrange
            const int applicantId = 101;

            // Act
            var response = await _client.DeleteAsync($"{ApiUrl}/{applicantId}");
            var content = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Contains("Applicant could not be found", content);
        }

        [Fact, TestPriority(2)]
        public async Task Delete_Applicant_When_Exist_Returns_Ok()
        {
            // Arrange
            var applicantId = InitialData.Applicants.First().Id;

            // Act
            var response = await _client.DeleteAsync($"{ApiUrl}/{applicantId}");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}