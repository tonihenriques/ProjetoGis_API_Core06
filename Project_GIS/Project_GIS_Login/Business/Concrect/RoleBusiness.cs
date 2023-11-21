using Project_GIS_Login.Business.Abstract;
using Project_GIS_Login.Entidade;
using Project_GIS_Login.Repository.Abstract;

namespace Project_GIS_Login.Business.Concrect
{
    public class RoleBusiness : BaseBusiness<Role>, IRolesBusiness
    {
        public RoleBusiness(IBaseRepository<Role> baseRepository) : base(baseRepository)
        {
        }

        public override void Inserir(Role entidade)
        {
            base.Inserir(entidade);
        }


    }
}
