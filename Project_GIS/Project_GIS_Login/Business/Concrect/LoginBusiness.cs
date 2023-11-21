using Project_GIS_Login.Business.Abstract;
using Project_GIS_Login.Entidade;
using Project_GIS_Login.Repository.Abstract;

namespace Project_GIS_Login.Business.Concrect
{
    public class LoginBusiness : BaseBusiness<User>, ILoginBusiness
    {
        public LoginBusiness(IBaseRepository<User> baseRepository) : base(baseRepository)
        {
        }

        public override void Inserir(User user)
        {
            base.Inserir(user);
        }



    }
}
