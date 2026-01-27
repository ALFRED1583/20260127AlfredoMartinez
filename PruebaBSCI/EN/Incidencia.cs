namespace PruebaBSCI.EN
{
    /// <summary>
    /// Informacion de incidencia
    /// </summary>
    public class Incidencia
    {
        public long IdIncidencia { get; set; }
        /// <summary>
        /// Titulo de incidencia
        /// </summary>
        public string Titulo { get; set; } = string.Empty;
        /// <summary>
        /// Descripcion de incidencia
        /// </summary>
        public string Descripcion { get; set; } = string.Empty;
        /// <summary>
        /// Id de la categoria
        /// </summary>
        public long IdCategoria { get; set; }
        /// <summary>
        /// Severidad
        /// </summary>
        public string Severidad { get; set; } = string.Empty;
        public DateTime FechaRegistro { get; set; }
        public string EstadoActual { get; set; } = string.Empty;
        public string? BitacoraInicial { get; set; }
        public List<Bitacora> Bitacoras { get; set; } = new();
    }
}
