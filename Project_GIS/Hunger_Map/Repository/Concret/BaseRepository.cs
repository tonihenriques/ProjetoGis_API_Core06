using Hunger_Map.Context;
using Hunger_Map.Entidade.Base;
using Hunger_Map.Repository.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Hunger_Map.Repository.Concret
{
    public class BaseRepository<T> : IBaseRepository<T> where T : EntidadeBase
    {
        protected HungerContext _context;

        public BaseRepository(HungerContext context)
        {
            _context = context;
        }

        public IQueryable<T> Consulta
        {
            get
            {
                return from c in _context.Set<T>() select c;
            }
        }

        public void Alterar(T entidade)
        {
            _context.Entry(entidade).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Excluir(T entidade)
        {
            entidade.DataExclusao = DateTime.Now;
            _context.Entry(entidade).State = EntityState.Deleted;
            _context.SaveChanges();
        }

        public void Inserir(T entidade)
        {
            try
            {
                entidade.id = Guid.NewGuid();
                entidade.DataInclusao = DateTime.Now;
                entidade.DataExclusao = DateTime.MaxValue;
                entidade.UsuarioInclusao = "teste";
                entidade.UsuarioExclusao = "nulo";


                _context.Entry(entidade).State = EntityState.Added;
                _context.SaveChanges();
            }
            catch (Exception e)
            {

                Console.WriteLine($"The file was not found: '{e}'");
            }

        }
    }
}
