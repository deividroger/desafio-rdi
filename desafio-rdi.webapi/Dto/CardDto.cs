using desafio_rdi.webapi.CustomValidators;

namespace desafio_rdi.webapi.Dto
{
    public class CardDto
    {       
        /// <summary>
        /// The CardNumber must to be long with 16 positions
        /// </summary>
        [Number(ErrorMessage = "cardNumber is mandatory", Positions = 16, Exact = true)]
        public long CardNumber { get; set; }

        /// <summary>
        /// The CVV number should up to be 4 positions
        /// </summary>
        [Number(ErrorMessage = "cvv is mandatory", Positions = 3, Exact =false)]
        public int CVV { get; set; }
    }
}
