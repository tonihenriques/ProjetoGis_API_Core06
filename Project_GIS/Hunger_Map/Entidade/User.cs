using Hunger_Map.Entidade.Base;

namespace Hunger_Map.Entidade
{
    public class User: EntidadeBase
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string  role { get; set; }
        public string PhoneNumber { get; set; }
        public string email { get; set; }
        public string Totalpessoas { get; set; }
        public string Menor10 { get; set; }
        public string Maior60 { get; set; }
        public string Feminino { get; set; }
        public string Masculino { get; set; }
    }
}
