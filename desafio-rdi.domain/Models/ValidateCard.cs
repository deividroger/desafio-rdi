using System;

namespace desafio_rdi.domain.Models
{
    public  class ValidateCard
    {
        public int CustomerId { get; set; }
        public Guid CardId { get; set; }
        public string Token { get; set; }
        public int CVV { get; set; }

    }
}
