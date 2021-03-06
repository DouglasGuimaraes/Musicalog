using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using Models.Entities;
using Newtonsoft.Json;
using Xunit;

namespace XUnitTest
{
    public class MuiscalogApiTest
    {
        private readonly static HttpClient _httpClient = new HttpClient();
        private const string _baseApiUrl = "http://localhost:5000/api/Album";
        private const string _healthCheckHeader = "HealthCheckHeader_IsAlive";
        private const int _albumId = 1;

        [Fact]
        public async void Test1_ShouldGetAlbumsFromApi()
        {
            var result = await _httpClient.GetAsync(_baseApiUrl);
            Assert.True(result.IsSuccessStatusCode);
        }

        [Fact]
        public async void Test2_ShouldCreateAlbumFromApi()
        {
            var albumToCreate = new Album
            {
                Title = "Album 1",
                ArtistName = "Arist 1",
                AlbumType = Models.Enum.AlbumType.CD,
                Stock = -1 // Invalid data, so we will receive an Unprocessable Entity but we can validate the header
            };

            var json = JsonConvert.SerializeObject(albumToCreate);
            var mediaType = "application/json";
            var data = new StringContent(json, Encoding.UTF8, mediaType);
            var result = await _httpClient.PostAsync(_baseApiUrl, data);
            var endpointAssert = IsEndpointWorking(result);
            Assert.True(endpointAssert);
        }

        [Fact]
        public async void Test3_ShouldGetAlbumByIdFromApi()
        {
            string finalUrl = $"{_baseApiUrl}/{_albumId}";
            var result = await _httpClient.GetAsync(finalUrl);
            var endpointAssert = IsEndpointWorking(result);
            Assert.True(endpointAssert);
        }

        [Fact]
        public async void Test4_ShouldUpdateAlbumFromApi()
        {
            var albumToUpdate = new Album
            {
                Id = 1,
                Title = "Album 1 Updated",
                ArtistName = "Arist 1 Updated",
                AlbumType = Models.Enum.AlbumType.Vinyl,
                Stock = -1 // Invalid data, so we will receive an Unprocessable Entity but we can validate the header
            };

            string finalUrl = $"{_baseApiUrl}/{_albumId}";
            var json = JsonConvert.SerializeObject(albumToUpdate);
            var mediaType = "application/json";
            var data = new StringContent(json, Encoding.UTF8, mediaType);
            var result = await _httpClient.PutAsync(finalUrl, data);

            var endpointAssert = IsEndpointWorking(result);
            Assert.True(endpointAssert);
        }

        [Fact]
        public async void Test5_ShouldDeleteAlbumFromApi()
        {
            string finalUrl = $"{_baseApiUrl}/{_albumId}";
            var result = await _httpClient.DeleteAsync(finalUrl);
            var endpointAssert = IsEndpointWorking(result);
            Assert.True(endpointAssert);
        }

        /// <summary>
        /// Validate endpoints based on the Response Header: HealthCheckHeader_IsAlive
        /// </summary>
        /// <param name="httpResponseMessage"></param>
        /// <returns></returns>
        private bool IsEndpointWorking(HttpResponseMessage httpResponseMessage)
        {
            var healthCheckHeader = httpResponseMessage.Headers.GetValues(_healthCheckHeader).ToList();
            if (healthCheckHeader.Count > 0)
                return true;
            return false;
        }

    }
}
