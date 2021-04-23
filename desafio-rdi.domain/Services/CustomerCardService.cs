using desafio_rdi.cross_cutting;
using desafio_rdi.domain.Models;
using desafio_rdi.domain.Repositories;
using desafio_rdi.domain.Validation;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace desafio_rdi.domain.Services
{

    public class CustomerCardService : ServiceBase, ICustomerCardService
    {
        private readonly IDesafioRepository _desafioRepository;
        private readonly ILogger<CustomerCardService> _logger;

        public CustomerCardService(INotification notification, IDesafioRepository desafioRepository, ILogger<CustomerCardService> logger) : base(notification)
        {
            _desafioRepository = desafioRepository;
            _logger = logger;
        }

        public async Task<CustomerCard> SaveCardAsync(CustomerCardRequest customerCardRequest)
        {
            var validation = new CustomerCardRequestValidation().Validate(customerCardRequest);

            if (!validation.IsValid)
            {
                AddErrors(validation);

                return null;
            }
            var customerCard = new CustomerCard(customerCardRequest.CustomerId, customerCardRequest.CardNumber, customerCardRequest.CVV);

            await _desafioRepository.SaveAsync(customerCard);

            return customerCard;
        }

        public bool Validate(ValidateCard  validateCard)    
        {
            var card = _desafioRepository.GetCardByIdAsync(validateCard.CardId);

            if (CardNotFound(card)) return false;

            if (CardBelongsToAnotherCustomer(validateCard, card)) return false;

            if (CardWasIssuedLongTimeAgo(card)) return false;

            if (TokenOrCVVInvalid(validateCard, card)) return false;

            _logger.LogInformation($"Card number {card.CardNumber}");

            return true;
        }

        private static bool CardWasIssuedLongTimeAgo(CustomerCard card)
        {
            return (DateTime.UtcNow - card.CreationDate).Minutes >= 30;
        }

        private static bool TokenOrCVVInvalid(ValidateCard validateCard, CustomerCard cardFound)
        {
            cardFound.GenerateCardToken();
            return (cardFound.Token != validateCard.Token) || (cardFound.CVV != validateCard.CVV);
        }

        private static bool CardBelongsToAnotherCustomer(ValidateCard validateCard, CustomerCard cardFound)
        {
            return validateCard.CustomerId != cardFound.CustomerId;
        }

        private static bool CardNotFound(CustomerCard cardFound)
        {
            return cardFound == null;
        }
    }
}