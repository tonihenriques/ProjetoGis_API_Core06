using Project_GIS_Login.Entidade.Base;

namespace Project_GIS_Login.Entidade
{
    public class User : EntidadeBase
    {

        public string Username { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string email { get; set; }
    }
}
