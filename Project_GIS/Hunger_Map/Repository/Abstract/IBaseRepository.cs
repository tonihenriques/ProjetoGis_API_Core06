using Hunger_Map.Entidade.Base;

namespace Hunger_Map.Repository.Abstract
{
   
        public interface IBaseRepository<T> where T : EntidadeBase
        {
            void Inserir(T entidade);

            void Alterar(T entidade);

            void Excluir(T entidade);


            IQueryable<T> Consulta { get; }
        }
    
}
