using desafio_rdi.domain.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace desafio_rdi.domain.Repositories
{

    public class DesafioRepository : IDesafioRepository
    {
        private readonly DesafioDbContext _desafioDbContext;

        public DesafioRepository(DesafioDbContext desafioDbContext)
        {
            _desafioDbContext = desafioDbContext;
        }

        public CustomerCard GetCardByIdAsync(Guid token)
        {
            return _desafioDbContext.CustomerCard.Where(x => x.CardId == token).SingleOrDefault();
        }

        public async Task  SaveAsync(CustomerCard customerCard)
        {
            _desafioDbContext.CustomerCard.Add(customerCard);

            await _desafioDbContext.SaveChangesAsync();

        }
    }
}