﻿using System.ComponentModel.DataAnnotations;


namespace ApiPeliculas.Models
{
    public class Categoria 
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }
        [Required]        
        public DateTime FechaCreacion { get; set; }

    }
}
