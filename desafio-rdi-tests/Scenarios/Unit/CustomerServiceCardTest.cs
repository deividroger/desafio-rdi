﻿using desafio_rdi.cross_cutting;
using desafio_rdi.domain.Models;
using desafio_rdi.domain.Repositories;
using desafio_rdi.domain.Services;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace desafio_rdi_tests.Scenarios.Unit
{
    public class CustomerServiceCardTest
    {
        private readonly Mock<INotification> _notification;
        private readonly Mock<IDesafioRepository> _repository;
        private readonly Mock<ILogger<CustomerCardService>> _logger;
        private readonly CustomerCardService _customerCardService;
        public CustomerServiceCardTest()
        {
            _notification = new Mock<INotification>();
            _repository = new Mock<IDesafioRepository>();
            _logger = new Mock<ILogger<CustomerCardService>>();
            _customerCardService = new CustomerCardService(_notification.Object, _repository.Object, _logger.Object);
        }

        [Fact]
        public async Task Should_Register_Card()
        {
            _repository.Setup(x => x.SaveAsync(It.IsAny<CustomerCard>())).Returns(() => Task.CompletedTask);

            var cardToRegister = new CustomerCardRequest()
            {
                CardNumber = 12345678901478529,
                CustomerId = 123,
                CVV = 480
            };
            var result = await _customerCardService.SaveCardAsync(cardToRegister);

            result.CardId.Should().NotBeEmpty();
            result.Token.Should().NotBeEmpty();

        }

        [Fact]
        public async Task Should_Not_Register_Card()
        {
            _repository.Setup(x => x.SaveAsync(It.IsAny<CustomerCard>())).Returns(() => Task.CompletedTask);

            var cardToRegister = new CustomerCardRequest();
            var result = await _customerCardService.SaveCardAsync(cardToRegister);

            result.Should().BeNull();
        }

        [Fact]
        public  void Should_Validate_Card_Successfully()
        {

            var cardRegistered = new CustomerCard()
            {
                CardId = Guid.NewGuid(),
                CardNumber = 7418529647524786,
                CreationDate = DateTime.UtcNow.AddMinutes(-5),
                CustomerId = 123,
                CVV = 134,
                Id = Guid.NewGuid(),
            };

            cardRegistered.GenerateCardToken();

            _repository.Setup(x => x.GetCardByIdAsync(It.IsAny<Guid>())).Returns(cardRegistered);

            var validate = new ValidateCard()
            {
                CardId = cardRegistered.CardId,
                CustomerId = cardRegistered.CustomerId,
                CVV = cardRegistered.CVV,
                Token = cardRegistered.Token
            };

            var valid = _customerCardService.Validate(validate);

            valid.Should().BeTrue();


        }

        [Fact]
        public void Should_Validate_Fail_Due_Card_Not_Found()
        {

            _repository.Setup(x => x.GetCardByIdAsync(It.IsAny<Guid>())).Returns((CustomerCard)null);

            var validate = new ValidateCard()
            {
                CardId = Guid.NewGuid(),
                CustomerId = 1,
                CVV = 234,
                Token = "134"
            };

            var valid = _customerCardService.Validate(validate);

            valid.Should().BeFalse();


        }

        [Fact]
        public void Should_Validate_Fail_Due_Invalid_Date()
        {

            var cardRegistered = new CustomerCard()
            {
                CardId = Guid.NewGuid(),
                CardNumber = 7418529647524786,
                CreationDate = DateTime.UtcNow.AddMinutes(-35),
                CustomerId = 123,
                CVV = 134,
                Id = Guid.NewGuid(),
            };

            cardRegistered.GenerateCardToken();

            _repository.Setup(x => x.GetCardByIdAsync(It.IsAny<Guid>())).Returns(cardRegistered);

            var validate = new ValidateCard()
            {
                CardId = cardRegistered.CardId,
                CustomerId = cardRegistered.CustomerId,
                CVV = cardRegistered.CVV,
                Token = cardRegistered.Token
            };

            var valid = _customerCardService.Validate(validate);

            valid.Should().BeFalse();


        }


        [Fact]
        public void Should_Validate_Fail_Due_Invalid_CVV()
        {

            var cardRegistered = new CustomerCard()
            {
                CardId = Guid.NewGuid(),
                CardNumber = 7418529647524786,
                CreationDate = DateTime.UtcNow.AddMinutes(-5),
                CustomerId = 123,
                CVV = 134,
                Id = Guid.NewGuid(),
            };

            cardRegistered.GenerateCardToken();

            _repository.Setup(x => x.GetCardByIdAsync(It.IsAny<Guid>())).Returns(cardRegistered);

            var validate = new ValidateCard()
            {
                CardId = cardRegistered.CardId,
                CustomerId = cardRegistered.CustomerId,
                CVV = 555,
                Token = cardRegistered.Token
            };

            var valid = _customerCardService.Validate(validate);

            valid.Should().BeFalse();


        }


        [Fact]
        public void Should_Validate_Fail_Due_Invalid_Token()
        {

            var cardRegistered = new CustomerCard()
            {
                CardId = Guid.NewGuid(),
                CardNumber = 7418529647524786,
                CreationDate = DateTime.UtcNow.AddMinutes(5),
                CustomerId = 123,
                CVV = 134,
                Id = Guid.NewGuid(),
            };

            cardRegistered.GenerateCardToken();

            _repository.Setup(x => x.GetCardByIdAsync(It.IsAny<Guid>())).Returns(cardRegistered);

            var validate = new ValidateCard()
            {
                CardId = cardRegistered.CardId,
                CustomerId = cardRegistered.CustomerId,
                CVV = cardRegistered.CVV,
                Token = "3"
            };

            var valid = _customerCardService.Validate(validate);

            valid.Should().BeFalse();

        }

        [Fact]
        public void Should_Validate_Fail_Due_Belongs_To_Another_Customer()
        {
            var cardRegistered = new CustomerCard()
            {
                CardId = Guid.NewGuid(),
                CardNumber = 7418529647524786,
                CreationDate = DateTime.UtcNow.AddMinutes(-35),
                CustomerId = 123,
                CVV = 134,
                Id = Guid.NewGuid(),
            };

            cardRegistered.GenerateCardToken();

            _repository.Setup(x => x.GetCardByIdAsync(It.IsAny<Guid>())).Returns(cardRegistered);

            var validate = new ValidateCard()
            {
                CardId = cardRegistered.CardId,
                CustomerId = 3,
                CVV = cardRegistered.CVV,
                Token = cardRegistered.Token
            };

            var valid = _customerCardService.Validate(validate);

            valid.Should().BeFalse();

        }
    }
}
