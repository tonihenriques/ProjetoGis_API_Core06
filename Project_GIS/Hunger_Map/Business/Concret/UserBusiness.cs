using Hunger_Map.Business.Abstract;
using Hunger_Map.Entidade;
using Hunger_Map.Repository.Abstract;

namespace Hunger_Map.Business.Concret
{
    public class UserBusiness : BaseBusiness<User>, IUserBusiness
    {
        public UserBusiness(IBaseRepository<User> baseRepository) : base(baseRepository)
        {
        }


        public override IQueryable<User> Consulta
        {
            get { return _baseRepository.Consulta; }
        }
    }
}
