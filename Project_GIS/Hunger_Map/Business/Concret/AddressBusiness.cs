using Hunger_Map.Business.Abstract;
using Hunger_Map.Entidade;
using Hunger_Map.Repository.Abstract;

namespace Hunger_Map.Business.Concret
{
    public class AddressBusiness : BaseBusiness<Address>, IAddressBusiness
    {
        public AddressBusiness(IBaseRepository<Address> baseRepository) : base(baseRepository)
        {
        }

        public override void Inserir(Address entidade)
        {
            base.Inserir(entidade);
        }

        public override IQueryable<Address> Consulta 
        {
            get { return _baseRepository.Consulta; }
        }

    }


}
