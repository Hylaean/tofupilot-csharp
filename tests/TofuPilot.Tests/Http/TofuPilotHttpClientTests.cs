using System.Net;
using System.Text.Json;
using FluentAssertions;
using TofuPilot.Abstractions.Exceptions;
using TofuPilot.Http;
using Xunit;

namespace TofuPilot.Tests.Http;

public class TofuPilotHttpClientTests
{
    private static readonly JsonSerializerOptions TestJsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    [Fact]
    public async Task GetAsync_Success_ReturnsDeserializedResponse()
    {
        // Arrange
        var expectedData = new TestResponse { Id = "123", Name = "Test" };
        var handler = new MockHttpMessageHandler(
            new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonSerializer.Serialize(expectedData, TestJsonOptions))
            });

        var httpClient = new HttpClient(handler) { BaseAddress = new Uri("https://test.com") };
        var client = new TofuPilotHttpClient(httpClient, TestJsonOptions);

        // Act
        var result = await client.GetAsync<TestResponse>("/test");

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be("123");
        result.Name.Should().Be("Test");
    }

    [Fact]
    public async Task GetAsync_NotFound_ThrowsNotFoundException()
    {
        // Arrange
        var handler = new MockHttpMessageHandler(
            new HttpResponseMessage(HttpStatusCode.NotFound)
            {
                Content = new StringContent("{\"message\":\"Not found\"}")
            });

        var httpClient = new HttpClient(handler) { BaseAddress = new Uri("https://test.com") };
        var client = new TofuPilotHttpClient(httpClient, TestJsonOptions);

        // Act & Assert
        var action = () => client.GetAsync<TestResponse>("/test");
        await action.Should().ThrowAsync<NotFoundException>()
            .WithMessage("Not found");
    }

    [Fact]
    public async Task GetAsync_BadRequest_ThrowsBadRequestException()
    {
        // Arrange
        var handler = new MockHttpMessageHandler(
            new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent("{\"error\":{\"message\":\"Invalid request\"}}")
            });

        var httpClient = new HttpClient(handler) { BaseAddress = new Uri("https://test.com") };
        var client = new TofuPilotHttpClient(httpClient, TestJsonOptions);

        // Act & Assert
        var action = () => client.GetAsync<TestResponse>("/test");
        await action.Should().ThrowAsync<BadRequestException>()
            .WithMessage("Invalid request");
    }

    [Fact]
    public async Task PostAsync_Success_ReturnsDeserializedResponse()
    {
        // Arrange
        var request = new TestRequest { Value = "test" };
        var expectedResponse = new TestResponse { Id = "123", Name = "Created" };
        var handler = new MockHttpMessageHandler(
            new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonSerializer.Serialize(expectedResponse, TestJsonOptions))
            });

        var httpClient = new HttpClient(handler) { BaseAddress = new Uri("https://test.com") };
        var client = new TofuPilotHttpClient(httpClient, TestJsonOptions);

        // Act
        var result = await client.PostAsync<TestRequest, TestResponse>("/test", request);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be("123");
    }

    private class TestRequest
    {
        public string? Value { get; init; }
    }

    private class TestResponse
    {
        public string? Id { get; init; }
        public string? Name { get; init; }
    }

    private class MockHttpMessageHandler : HttpMessageHandler
    {
        private readonly HttpResponseMessage _response;

        public MockHttpMessageHandler(HttpResponseMessage response)
        {
            _response = response;
        }

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            return Task.FromResult(_response);
        }
    }
}
