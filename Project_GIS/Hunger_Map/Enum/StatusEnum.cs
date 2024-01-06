using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Hunger_Map.Enum
{
    public enum StatusEnum
    {
        [Display(Name = "Não entregue")]
        Nao_entregue = 1,

        [Display(Name = "Entregue")]
        Entregue = 2,

    }
}
