using Project_GIS_Login.Entidade.Base;

namespace Project_GIS_Login.Entidade
{
    public class UserRoles : EntidadeBase
    {

        public Guid idUser { get; set; }
        public Guid idRole { get; set; }
    }
}
