namespace ClinicaAmanecer.DTOS
{
    public class PacienteTableDto
    {
        public int PacienteID { get; set; }
        public string NumeroExpediente { get; set; } = string.Empty;
        public string Cedula { get; set; } = string.Empty;
        public string NombreCompleto { get; set; } = string.Empty;
        public string TipoSangre { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string ResumenAlergias { get; set; } = "Ninguno";
        public bool Estado { get; set; }
    }
}