using System.Collections.Generic;

namespace desafio_rdi.cross_cutting
{
    public interface INotification
    {
        IEnumerable<string> GetAll();

        void Clear();

        void Add(string notification);

        bool Any();
    }
}
