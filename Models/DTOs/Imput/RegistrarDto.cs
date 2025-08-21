namespace SrSur.Models.DTOs.Imput
{

    public class RegistrarDto : DTOGeneric
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
    }

}
