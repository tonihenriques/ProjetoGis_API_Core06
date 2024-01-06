using Hunger_Map.Business.Abstract;
using Hunger_Map.Entidade;
using Hunger_Map.Repository.Abstract;

namespace Hunger_Map.Business.Concret
{
    public class ListAlimentosBusiness : BaseBusiness<ListAlimentos>, IListAlimentosBusiness
    {
        public ListAlimentosBusiness(IBaseRepository<ListAlimentos> baseRepository) : base(baseRepository)
        {
        }

        public override void Inserir(ListAlimentos entidade)
        {
            base.Inserir(entidade);
        }

        public override IQueryable<ListAlimentos> Consulta
        {
            get { return _baseRepository.Consulta; }
        }
    }
}
