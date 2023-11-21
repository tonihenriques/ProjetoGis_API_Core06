using Project_GIS_Login.Context;
using Project_GIS_Login.Entidade.Base;
using Project_GIS_Login.Repository.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Project_GIS_Login.Repository.Concrect
{
    public class BaseRepository<T> : IBaseRepository<T> where T : EntidadeBase
    {
        protected LoginContext _loginContext;
        public BaseRepository(LoginContext loginContext)
        {
            _loginContext = loginContext;

        }

        public void Inserir(T entidade)
        {
            entidade.id = Guid.NewGuid();
            entidade.DataInclusao = DateTime.Now;
            entidade.DataExclusao = DateTime.MaxValue;

            _loginContext.Entry(entidade).State = EntityState.Added;
            _loginContext.SaveChanges();



        }

        public void Alterar(T entidade)
        {
            _loginContext.Entry(entidade).State = EntityState.Modified;
            _loginContext.SaveChanges();
        }

        public IQueryable<T> Consulta
        {
            get
            {
                return from c in _loginContext.Set<T>() select c;
            }
        }



        public void Excluir(T entidade)
        {
            entidade.DataExclusao = DateTime.Now;
            _loginContext.Entry(entidade).State = EntityState.Deleted;
            _loginContext.SaveChanges();
        }


    }
}
