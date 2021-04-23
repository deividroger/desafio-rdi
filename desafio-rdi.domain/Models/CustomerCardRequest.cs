namespace desafio_rdi.domain.Models
{
    public class CustomerCardRequest
    {

        public int CustomerId { get; set; }

        public long CardNumber { get; set; }

        public int CVV { get; set; }
    }
}