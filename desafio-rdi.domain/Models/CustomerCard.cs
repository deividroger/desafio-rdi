using desafio_rdi.domain.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
namespace desafio_rdi.domain.Models
{
    public class CustomerCard
    {
        public CustomerCard()
        {

        }

        public CustomerCard(int customerId, long cardNumber, int cvv)
        {
            CustomerId = customerId;
            CardNumber = cardNumber;
            
            CVV = cvv;

            CreationDate = DateTime.UtcNow;
            CardId = Guid.NewGuid();
            Id = Guid.NewGuid();
            
            GenerateCardToken();
        }

        [Key]
        public Guid Id { get; set; }

        public DateTime CreationDate { get; set; }

        public string Token { get; set; }

        public Guid CardId { get; set; }

        public int CustomerId { get; set; }

        public long CardNumber { get; set; }

        public int CVV { get; set; }


        public void GenerateCardToken()
        {
            var cardPositionArray = new List<int>();
            
            int rotationTimes = 2;

            var lastFourPosition = CardNumber.ToString().ToCharArray(12, 4).ToList();

            lastFourPosition.ForEach(x => cardPositionArray.Add(int.Parse(x.ToString())));

            Token =  ArrayRotation.RightRotate(cardPositionArray.ToArray(), rotationTimes);
        }
    }
}
