using System.ComponentModel.DataAnnotations;

namespace ApiPeliculas.Models.DataTransferObjects
{
    //Esta entidad se sencarga de: Leer los registros,para leer un registro individual y  Actualizar.
    //Otra entidad se encargará de Crear (porque para Crear solo neesitamos la Property Nombre)
    public class CategoriaDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100, ErrorMessage = "El maximo de caracteres es 100")]
        public string Nombre { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
