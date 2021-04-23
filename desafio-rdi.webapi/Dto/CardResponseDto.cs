using System;

namespace desafio_rdi.webapi.Dto
{
    public class CardResponseDto
    {
        /// <summary>
        /// The UTC date of stored card
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// The token generated during the process
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// The cardId generated during the process
        /// </summary>
        public Guid CardId { get; set; }

    }
}