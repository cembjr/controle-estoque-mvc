using CB.Core.DomainObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CB.Core.Data
{
    public interface IRepository<T> : IDisposable 
        where T : class, IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
        Task<IEnumerable<T>> Listar(Expression<Func<T, bool>> filtro = null);
        Task<T> Obter(Expression<Func<T, bool>> filtro);
        Task<T> ObterPorId(Guid id);

        void Adicionar(T ent);
        void Atualizar(T ent);
        void Deletar(T ent);
    }
}
