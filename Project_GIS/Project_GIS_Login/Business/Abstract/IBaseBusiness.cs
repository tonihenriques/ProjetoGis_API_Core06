using Project_GIS_Login.Entidade.Base;

namespace Project_GIS_Login.Business.Abstract
{
    public interface IBaseBusiness
    {
        public interface IBaseBusiness<T> where T : EntidadeBase
        {
            void Inserir(T entidade);

            void Alterar(T entidade);

            void Excluir(T entidade);

            IQueryable<T> Consulta { get; }
        }
    }
}
