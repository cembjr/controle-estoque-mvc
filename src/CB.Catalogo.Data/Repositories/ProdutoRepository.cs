using CB.Catalogo.Data.Context;
using CB.Catalogo.Domain.Entities;
using CB.Catalogo.Domain.Repositories;
using CB.Core.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CB.Catalogo.Data.Repositories
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(CatalogoContext db) : base(db)
        {            
        }

        public override async Task<IEnumerable<Produto>> Listar(Expression<Func<Produto, bool>> filtro = null)
        {
            var lstRet = filtro == null ? DbSet.AsQueryable() : DbSet.Where(filtro).AsQueryable();
            return await lstRet.Include(f => f.Movimentacoes).ToListAsync();
        }

    }
}
