using Hunger_Map.Entidade.Base;

namespace Hunger_Map.Entidade
{
    public class Address: EntidadeBase
    {
        public string rua { get; set; }
        public string numero { get; set; }
        public string bairro { get; set; }
        public string cidade { get; set; }
        public string estado { get; set; }
        public string pais { get; set; }
        public string cep { get; set; }
        public string email { get; set; }
        public string lat { get; set; }
        public string lng { get; set; }

      

    }
}
