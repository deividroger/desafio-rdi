using desafio_rdi.domain.Models;
using System;
using System.Threading.Tasks;

namespace desafio_rdi.domain.Repositories
{
    public interface IDesafioRepository
    {
        CustomerCard GetCardByIdAsync(Guid token);

        Task SaveAsync(CustomerCard customerCard);
    }
}