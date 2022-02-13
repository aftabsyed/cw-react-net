using System;
using Xunit;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using CWRetail;
using CWRetail.Model;
using CWRetail.UnitTests.Extensions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using AutoFixture.Xunit2;
using CWRetail.DTO;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;


namespace CWRetail.UnitTests
{
    public class ProductControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly IFixture _fixture;
        private readonly WebApplicationFactory<Startup> _factory;

        public ProductControllerTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _fixture = new Fixture();
        }
        
        [Theory]
        [AutoData]
        public async Task GivenInvalidProductId_WhenGettingProductById_ThenReturn404NotFound([Range(-99, 0)] int Id)
        {
            // Arrange
            var client = _factory.CreateClient();
            var url = $"{client.BaseAddress}api/Product/{Id}";

            // Act
            var responseMessage = await client.GetAsync(url);

            // Assert
            responseMessage.StatusCode.Should()
                .Be(StatusCodes.Status404NotFound);
        }


        // TEST NAME - createProduct
        // TEST DESCRIPTION - Create Product should succeed
        [Fact]
        public async Task GivenValidProduct_WhenCreatingNewProduct_ThenReturnCreatedProduct()
        {
            // Arrange
            HttpClient client = _factory.CreateClient();
            var url = $"{client.BaseAddress}api/product";
            var command = _fixture.Create<ProductDto>();
            string stringContent = JsonConvert.SerializeObject(command);

            // Act
            HttpResponseMessage responseMessage = await client.PostAsync(url, new StringContent(stringContent, Encoding.UTF8, MediaTypeNames.Application.Json));

            // Assert
            responseMessage.EnsureSuccessStatusCode();
            var product = await responseMessage.DeserializeContentAsync<Product>();

            var getUrl = $"{client.BaseAddress}api/product/{product.Id}";
            HttpResponseMessage getResponse = await client.GetAsync(getUrl);
            getResponse.EnsureSuccessStatusCode();

            var storedProduct = await getResponse.DeserializeContentAsync<Product>();

            storedProduct.Should().BeEquivalentTo(product);

            product.Id.Should()
                .BeGreaterThan(0);

            product.Name.Should()
                .Be(command.Name);

            product.Type.Should()
                .Be(command.Type);

            product.Price.Should()
                .Be(command.Price);

            product.Active.Should()
                .Be(command.Active);
        }

        [Fact]
        public async Task GivenValidProductId_WhenDeletingProduct_ThenReturn204()
        {
            // Arrange
            HttpClient client = _factory.CreateClient();
            var url = $"{client.BaseAddress}api/product/1";
            HttpResponseMessage getResponse = await client.GetAsync(url);
            var expected = await getResponse.DeserializeContentAsync<ProductDto>();

            // Act
            HttpResponseMessage responseMessage = await client.DeleteAsync(url);

            // Assert
            responseMessage.EnsureSuccessStatusCode();
            var deletedProduct = await responseMessage.DeserializeContentAsync<ProductDto>();
            responseMessage.StatusCode.Should().Be(StatusCodes.Status204NoContent);
        }
    }
}