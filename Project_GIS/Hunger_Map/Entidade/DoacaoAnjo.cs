using Hunger_Map.Entidade.Base;
using Hunger_Map.Enum;

namespace Hunger_Map.Entidade
{
    public class DoacaoAnjo: EntidadeBase
    {
        public string idItem { get; set; }
        public string idAnjo { get; set; }
        public string qtidade { get; set; }
        public StatusEnum status { get; set; }

    }
}
