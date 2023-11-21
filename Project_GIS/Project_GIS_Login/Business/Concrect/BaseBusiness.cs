using Project_GIS_Login.Entidade.Base;
using Project_GIS_Login.Repository.Abstract;
using static Project_GIS_Login.Business.Abstract.IBaseBusiness;

namespace Project_GIS_Login.Business.Concrect
{
    public class BaseBusiness<T> : IBaseBusiness<T> where T : EntidadeBase
    {
        protected IBaseRepository<T> _baseRepository;
        public BaseBusiness(IBaseRepository<T> baseRepository)
        {
            _baseRepository = baseRepository;
        }


        public virtual void Inserir(T entidade)
        {
            _baseRepository.Inserir(entidade);

        }
        public virtual IQueryable<T> Consulta
        {
            get { return _baseRepository.Consulta; }
        }


        public virtual void Alterar(T entidade)
        {
            _baseRepository.Alterar(entidade);
        }

        public virtual void Excluir(T entidade)
        {
            _baseRepository.Excluir(entidade);
        }

    }
}
