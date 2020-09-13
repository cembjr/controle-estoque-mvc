using CB.Core.DomainObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CB.Core.Data
{
    public class Repository<T> : IRepository<T>
        where T : class, IAggregateRoot
    {
        protected readonly DbContext Db;
        protected readonly DbSet<T> DbSet;

        public Repository(DbContext db)
        {
            this.Db = db;
            this.DbSet = db.Set<T>();
        }

        public IUnitOfWork UnitOfWork => (IUnitOfWork) Db;
        public virtual async Task<IEnumerable<T>> Listar(Expression<Func<T, bool>> filtro = null)
            => filtro == null ? await DbSet.ToListAsync() : await DbSet.Where(filtro).ToListAsync();

        public virtual async Task<T> Obter(Expression<Func<T, bool>> filtro) => await DbSet.FirstOrDefaultAsync(filtro);

        public virtual async Task<T> ObterPorId(Guid id) => await DbSet.FindAsync(id);

        public void Adicionar(T ent) => DbSet.Add(ent);

        public void Atualizar(T ent) => DbSet.Update(ent);

        public void Deletar(T ent) => DbSet.Remove(ent);

        public void Dispose() => Db.Dispose();

    }
}
