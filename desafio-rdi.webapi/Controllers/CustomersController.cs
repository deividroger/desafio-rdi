using AutoMapper;
using desafio_rdi.cross_cutting;
using desafio_rdi.domain.Models;
using desafio_rdi.domain.Services;
using desafio_rdi.webapi.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace desafio_rdi.webapi.Controllers
{
    [Route("api/[controller]")]

    public class CustomersController : BaseControllerApi
    {
        private readonly ICustomerCardService _customerCardService;
        private readonly IMapper _mapper;

        public CustomersController(ICustomerCardService customerCardService, INotification notification, IMapper mapper) : base(notification)
        {
            _customerCardService = customerCardService;
            _mapper = mapper;
        }

        /// <summary>
        /// operation responsible for storing the card 
        /// </summary>
        /// <param name="customerCardRequest">The card's data that will be stored</param>
        /// <param name="customerId">The custumerId of your client</param>
        /// <returns></returns>
        [HttpPost("{customerId}/cards")]
        [ProducesResponseType(typeof(CardResponseDto), 200)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string), 500)]
       
        public async Task<IActionResult> SaveCard([FromBody] CardDto customerCardRequest, int customerId)
        {
            var request = _mapper.Map<CustomerCardRequest>(customerCardRequest);
            request.CustomerId = customerId;

            var response = await _customerCardService.SaveCardAsync(request);
            var result = _mapper.Map<CardResponseDto>(response);

            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(result);
        }
        /// <summary>
        /// operation responsible for validating the card
        /// </summary>
        /// <param name="validateCardRequest">The data of the storaged card</param>
        /// <param name="customerId">The custumerId of your client</param>
        /// <param name="cardId">The cardId of your card</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string), 500)]
        [HttpPost("{customerId}/cards/{cardId}/validate")]
        public IActionResult ValidateCard(ValidateCardRequestDto validateCardRequest, int customerId, Guid cardId)
        {
            var request = _mapper.Map<ValidateCard>(validateCardRequest);
            request.CustomerId = customerId;
            request.CardId = cardId;

            var response = _customerCardService.Validate(request);

            return CustomResponse(response);
        }
    }
}
