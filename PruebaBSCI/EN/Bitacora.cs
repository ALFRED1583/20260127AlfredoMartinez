namespace PruebaBSCI.EN
{
    public class Bitacora
    {
        public long IdBitacora { get; set; }
        public long IdIncidencia { get; set; }
        public DateTime FechaHora { get; set; }
        public string AccionRealizada { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public string Comentario { get; set; } = string.Empty;
        public string Usuario { get; set; } = string.Empty;

    }
}
