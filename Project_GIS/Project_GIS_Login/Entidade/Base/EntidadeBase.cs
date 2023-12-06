namespace Project_GIS_Login.Entidade.Base
{
    public class EntidadeBase
    {
        public Guid id { get; set; }

        public DateTime DataInclusao { get; set; }

        public string UsuarioInclusao { get; set; }

        public string UsuarioExclusao { get; set; }

        public DateTime DataExclusao { get; set; }
    }
}
