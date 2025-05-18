using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Application.DTOs;

public class ClientsControllerTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public ClientsControllerTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task CreateClient_ShouldReturn201Created()
    {
        var client = new ClientCreateDto
        {
            Name = "Ana Ruiz",
            IdentificationNumber = "123456789",
            IdentificationType = 1,
            Email = "ana.ruiz@example.com",
            Phone = "+57 3001234567",
            BirthDate = new DateTime(1990, 5, 10)
        };

        var response = await _client.PostAsJsonAsync("/api/v1/clients", client);

        response.StatusCode.Should().Be(HttpStatusCode.Created);
        response.Headers.Location.Should().NotBeNull();
    }

    [Fact]
    public async Task GetClientById_ShouldReturnClient()
    {
        // Arrange
        var client = new ClientCreateDto
        {
            Name = "Ana Ruiz",
            IdentificationNumber = "123456789",
            IdentificationType = 1,
            Email = "ana.ruiz@example.com",
            Phone = "+57 3001234567",
            BirthDate = new DateTime(1990, 5, 10)
        };

        // Act - POST
        var post = await _client.PostAsJsonAsync("/api/v1/clients", client);
        var id = ExtractIdFromLocation(post);

        // Act - GET
        var response = await _client.GetAsync($"/api/v1/clients/{id}");
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var responseBody = await response.Content.ReadAsStringAsync();

        // Assert - Validar contenido del body como string
        responseBody.Should().Contain("Ana Ruiz");
        responseBody.Should().Contain("123456789");
        responseBody.Should().Contain("ana.ruiz@example.com");
        responseBody.Should().Contain("+57 3001234567");
        responseBody.Should().Contain("identificationType");
        responseBody.Should().Contain("createdAt");
        responseBody.Should().Contain("updatedAt");
        responseBody.Should().Contain("age");
    }

    [Fact]
    public async Task UpdateClient_ShouldReturn200OK()
    {
        var client = new ClientCreateDto
        {
            Name = "Ana Ruiz",
            IdentificationNumber = "123456789",
            IdentificationType = 1,
            Email = "ana.ruiz@example.com",
            Phone = "+57 3001234567",
            BirthDate = new DateTime(1990, 5, 10)
        };

        var post = await _client.PostAsJsonAsync("/api/v1/clients", client);
        var id = ExtractIdFromLocation(post);

        var updatedClient = new ClientUpdateDto
        {
            Name = "Ana Ruiz Updated",
            Email = "ana.updated@example.com",
            Phone = "+57 3107654321",
            BirthDate = new DateTime(1990, 5, 10)
        };

        var response = await _client.PutAsJsonAsync($"/api/v1/clients/{id}", updatedClient);
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task DeleteClient_ShouldReturn200OK_AndNotFoundAfter()
    {
        var client = new ClientCreateDto
        {
            Name = "Ana Ruiz",
            IdentificationNumber = "123456789",
            IdentificationType = 1,
            Email = "ana.ruiz@example.com",
            Phone = "+57 3001234567",
            BirthDate = new DateTime(1990, 5, 10)
        };

        var post = await _client.PostAsJsonAsync("/api/v1/clients", client);
        var id = ExtractIdFromLocation(post);

        var delete = await _client.DeleteAsync($"/api/v1/clients/{id}");
        delete.StatusCode.Should().Be(HttpStatusCode.NoContent);

        var getAfterDelete = await _client.GetAsync($"/api/v1/clients/{id}");
        getAfterDelete.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    private int ExtractIdFromLocation(HttpResponseMessage response)
    {
        var location = response.Headers.Location?.ToString();
        location.Should().NotBeNull();
        var idStr = location!.Split("/").Last();
        int.TryParse(idStr, out var id).Should().BeTrue();
        return id;
    }
}
