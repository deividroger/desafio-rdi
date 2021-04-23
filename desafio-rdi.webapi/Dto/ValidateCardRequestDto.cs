using desafio_rdi.webapi.CustomValidators;
using System;
using System.ComponentModel.DataAnnotations;

namespace desafio_rdi.webapi.Dto
{
    public class ValidateCardRequestDto
    {
        /// <summary>
        /// the token of your stored card
        /// </summary>
        [Required(ErrorMessage ="token is mandatory")]
        public string Token { get; set; }

        /// <summary>
        /// The cvv number of your card
        /// </summary>
        [Number(ErrorMessage = "cvv is mandatory",Positions =5)]
        public int CVV { get; set; }

    }
}