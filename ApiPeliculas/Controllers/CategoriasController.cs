using ApiPeliculas.Models;
using ApiPeliculas.Models.DataTransferObjects;
using ApiPeliculas.Repositorio.IRepositorio;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace ApiPeliculas.Controllers
{
    //[Route("api/[controller]")] opcion por defecto
    [Route("api/categorias")] // opcion dinamica
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriaCrudRepositorio _ctRepo;
        private readonly IMapper _mapper;

        public CategoriasController(ICategoriaCrudRepositorio ctRepo, IMapper maper)
        {
            _ctRepo = ctRepo;
            _mapper = maper;
        }




        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetCayegorias()
        {
            List<Categoria> listaCategorias = _ctRepo.GetCategorias().ToList();

            List<CategoriaDto> listaCategoriasDto = new List<CategoriaDto>();// Vamos a exponer el Dto en lugar de la Lista de Categorias directamente

            foreach (var categoria in listaCategorias)
            {
                listaCategoriasDto.Add(_mapper.Map<CategoriaDto>(categoria));
            }
            return Ok(listaCategoriasDto);
        }



        [HttpGet("{idCategoriaABuscar:int}", Name = "GetCategoria")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetCategoria(int idCategoriaABuscar)
        {
            Categoria categoriaEncontrada = _ctRepo.GetCategoria(idCategoriaABuscar);

            if (categoriaEncontrada == null)
            {
                return NotFound();
            }

            CategoriaDto CategoriaEncontradaDto = _mapper.Map<CategoriaDto>(categoriaEncontrada);
            return Ok(CategoriaEncontradaDto);
        }
    }   
    
}
