using Hunger_Map.Entidade.Base;

namespace Hunger_Map.Business.Abstract
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
