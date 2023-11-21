using Project_GIS_Login.Business.Abstract;
using Project_GIS_Login.Entidade;
using Project_GIS_Login.Repository.Abstract;

namespace Project_GIS_Login.Business.Concrect
{
    public class UserRolesBusiness : BaseBusiness<UserRoles>, IUserRolesBusiness
    {
        public UserRolesBusiness(IBaseRepository<UserRoles> baseRepository) : base(baseRepository)
        {
        }

        public override void Inserir(UserRoles userRoles)
        {
            userRoles.UsuarioExclusao = "teste";

            base.Inserir(userRoles);
        }
    }
}
