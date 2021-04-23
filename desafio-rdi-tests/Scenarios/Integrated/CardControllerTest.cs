
using desafio_rdi.webapi.Dto;
using desafio_rdi_tests.Fixtures;
using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
namespace desafio_rdi_tests.Scenarios.Integrated
{
    public class CardControllerTest
    {
        private readonly TestContext _testContext;

        public CardControllerTest()
        {
            _testContext = new TestContext();
        }

        [Fact]
        public async Task Card_Post_Returns_BadRequest()
        {
            var response = await InvokeCreateCard(0, 0, 0);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Card_Post_Returns_OK()
        {
            var response = await InvokeCreateCard(134, 5361513396360376, new Random().Next(100, 1000));

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var cardSaved = JsonConvert.DeserializeObject<CardResponseDto>(await response.Content.ReadAsStringAsync());

            Assert.NotNull(cardSaved);

            cardSaved.CardId.Should().NotBeEmpty();
            cardSaved.Token.Should().NotBeEmpty();

        }

        [Fact]
        public async Task Valid_Post_Returns_Validate_True()
        {
            var cvv = new Random().Next(100, 1000);
            var customerId = new Random().Next(100, 1000);
            var cardNumber = 5361513396360376;

            var response = await InvokeCreateCard(customerId, cardNumber, cvv);
            var card = JsonConvert.DeserializeObject<CardResponseDto>(await response.Content.ReadAsStringAsync());

            
            var responseValidate = await InvokeValidateCard(cvv, customerId, card);
            responseValidate.StatusCode.Should().Be(HttpStatusCode.OK);
            var validated = JsonConvert.DeserializeObject<bool>(await responseValidate.Content.ReadAsStringAsync());

            Assert.True(validated);

        }

        [Fact]
        public async Task Valid_Post_Returns_Validate_False()
        {
            var cvv = new Random().Next(100, 1000);
            var customerId = new Random().Next(100, 1000);
            var cardNumber = 5361513396360377;

            var response = await InvokeCreateCard(customerId, cardNumber, cvv);
            var card = JsonConvert.DeserializeObject<CardResponseDto>(await response.Content.ReadAsStringAsync());


            var responseValidate = await InvokeValidateCard(123, customerId, card);
            responseValidate.StatusCode.Should().Be(HttpStatusCode.OK);
            var validated = JsonConvert.DeserializeObject<bool>(await responseValidate.Content.ReadAsStringAsync());

            Assert.False (validated);

        }

        [Fact]
        public async Task Valid_Post_Returns_BadRequest()
        {

            var card = new CardResponseDto();

            var responseValidate = await InvokeValidateCard(0, 0, card);
            responseValidate.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        }

        private async Task<HttpResponseMessage> InvokeValidateCard(int cvv, int customerId, CardResponseDto card)
        {
            var validateDto = new ValidateCardRequestDto()
            {
                CVV = cvv,
                Token = card.Token
            };

            var json = JsonConvert.SerializeObject(validateDto);

            var responseValidate = await _testContext.Client.PostAsync($"/api/customers/{customerId}/cards/{card.CardId}/validate", new StringContent(json, Encoding.UTF8, "application/json"));
            return responseValidate;
        }

        private async Task<HttpResponseMessage> InvokeCreateCard(int customerId, long cardNumber, int cvv)
        {
            var customerCardDtoRequest = new CardDto()
            {
                CardNumber = cardNumber,
                CVV = cvv,
            };

            var json = JsonConvert.SerializeObject(customerCardDtoRequest);

            var response = await _testContext.Client.PostAsync($"/api/customers/{customerId}/cards", new StringContent(json, Encoding.UTF8, "application/json"));
            return response;
        }
    }
}
