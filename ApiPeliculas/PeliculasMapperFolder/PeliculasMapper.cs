using ApiPeliculas.Models;
using ApiPeliculas.Models.DataTransferObjects;
using AutoMapper;
using System.ComponentModel;

namespace ApiPeliculas.PeliculasMapper
{
    public class PeliculasMapperProfile : Profile
    {
        public PeliculasMapperProfile()
        {
            // Configura el mapeo bidireccional entre Categoria y CategoriaDto,
            // permitiendo la conversión de Categoria a CategoriaDto y viceversa.
            CreateMap<Categoria, CategoriaDto>().ReverseMap();
            //
            CreateMap<Categoria, CategoriaDto>().ReverseMap();
        }
    }
}
