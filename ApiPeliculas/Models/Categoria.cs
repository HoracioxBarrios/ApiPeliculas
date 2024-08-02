using System.ComponentModel.DataAnnotations;


namespace ApiPeliculas.Models
{
    //No se va a Exponer esto sino el DTO
    public class Categoria 
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]        
        public DateTime FechaCreacion { get; set; }

    }
}
