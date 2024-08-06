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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CrearCategoria([FromBody] CrearCategoriaDto crearCategoriaDto) 
        {
            IActionResult resultado = null;

            if(!ModelState.IsValid) //si no es valido el estado
            {
                resultado = BadRequest(ModelState); // devuelve un BadRequestObjectResult que a la vez es un IActionResult
            }

            if(crearCategoriaDto == null) 
            { 
                resultado = NotFound(ModelState);
            }

            if (_ctRepo.ExisteCategoria(crearCategoriaDto.Nombre)) 
            {
                ModelState.AddModelError("", "La Categoria ya existe.");
                resultado = StatusCode(404, ModelState);
            }

            Categoria categoria = _mapper.Map<Categoria>(crearCategoriaDto);
            if (_ctRepo.CrearCategoria(categoria))
            {
                ModelState.AddModelError("", $"Algo salio mal.{categoria.Nombre}");
                resultado = StatusCode(404, ModelState);
            }
            resultado = CreatedAtRoute(
                "GetCategoria", new {categoriaId = categoria.Id}, categoria);

            return resultado;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetCayegorias()
        {
            List<Categoria> listaCategorias = _ctRepo.GetCategorias().ToList();

            List<CategoriaDto> listaCategoriasDto = new List<CategoriaDto>();// Vamos a exponer el Dto en lugar de la Lista de Categorias directamente

            foreach (Categoria categoria in listaCategorias)
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
            IActionResult resultado = null;
            Categoria categoriaEncontrada = _ctRepo.GetCategoria(idCategoriaABuscar);

            if (categoriaEncontrada == null)
            {
                resultado = NotFound();
            }

            CategoriaDto CategoriaEncontradaDto = _mapper.Map<CategoriaDto>(categoriaEncontrada);

            resultado = Ok(CategoriaEncontradaDto);
            return resultado;
        }
    }   
    
}
