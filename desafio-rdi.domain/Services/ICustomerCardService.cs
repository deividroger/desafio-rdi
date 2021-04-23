using desafio_rdi.domain.Models;
using System.Threading.Tasks;

namespace desafio_rdi.domain.Services
{
    public interface ICustomerCardService
    {
        Task<CustomerCard> SaveCardAsync(CustomerCardRequest customerCard);
        bool Validate(ValidateCard customerCard);
    }
}