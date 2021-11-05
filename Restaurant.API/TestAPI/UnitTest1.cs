using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace TestAPI
{
    public class UnitTest1
    {
        private readonly HttpClient _httpClient;

        public UnitTest1()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:5000/api/");
        }

        [Fact]
        public async Task TestInvalidDate()
        {
            var response = await _httpClient.GetAsync("hours/aa:00");

            var result = await response.Content.ReadAsStringAsync();

            Assert.Equal("Formato da hora inválido. Favor informar hora no padrão HH:mm", result);
        }

        [Fact]
        public async Task TestReturnValues()
        {
            
            var response = await _httpClient.GetAsync("hours/22:00");

            var result = await response.Content.ReadAsStringAsync();
            
            Assert.Contains("Sapporo-Ya Japanese Restaurant", result);
        }

        [Fact]
        public async Task TestNotFound()
        {

            var response = await _httpClient.GetAsync("hours/23:40");

            Assert.Equal("[]", await response.Content.ReadAsStringAsync());
        }
    }
}
