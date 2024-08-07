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

            if (!ModelState.IsValid) //si no es valido el estado
            {
                return BadRequest(ModelState); // devuelve un BadRequestObjectResult que a la vez es un IActionResult
            }

            if (crearCategoriaDto == null)
            {
                return NotFound(ModelState);
            }

            if (_ctRepo.ExisteCategoria(crearCategoriaDto.Nombre))
            {
                ModelState.AddModelError("", "La Categoria ya existe.");
                return StatusCode(404, ModelState);
            }

            var categoria = _mapper.Map<Categoria>(crearCategoriaDto);

            if (!_ctRepo.CrearCategoria(categoria))
            {
                ModelState.AddModelError("", $"Algo salio mal.{categoria.Nombre}");
                return StatusCode(404, ModelState);
            }
            IActionResult result = CreatedAtRoute("GetCategoria", new { idCategoriaABuscar = categoria.Id }, categoria);
            return result;
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

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetCategorias()
        {
            List<Categoria> listaCategorias = _ctRepo.GetCategorias().ToList(); //Lista Original (Que no se expone)
            //Si quisiera mostrar las categorias aca deberia ordenarlas antes de mapearlas a la class que se va a EXPONER
            listaCategorias = listaCategorias.OrderBy(c => c.Id).ToList();
            //listaCategorias = listaCategorias.OrderBy(c => c.Nombre).ToList();

            List<CategoriaDto> listaCategoriasDto = new List<CategoriaDto>();// Vamos a exponer el Dto en lugar de la Lista de Categorias directamente

            foreach (Categoria categoria in listaCategorias)
            {
                listaCategoriasDto.Add(_mapper.Map<CategoriaDto>(categoria));
            }
            return Ok(listaCategoriasDto);
        }


        [HttpPatch("{idCategoriaABuscar:int}", Name = "ActualizarPatchCategoria")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult ActualizarPatchCategoria(
            int idCategoriaABuscar, [FromBody] CategoriaDto categoriaDto)
        {

            if (!ModelState.IsValid) //si no es valido el estado
            {
                return BadRequest(ModelState); // devuelve un BadRequestObjectResult que a la vez es un IActionResult
            }

            if (categoriaDto == null || idCategoriaABuscar != categoriaDto.Id)
            {
                return BadRequest(ModelState);
            }

            var categoria = _mapper.Map<Categoria>(categoriaDto);

            if (!_ctRepo.ActualizarCategoria(categoria))
            {
                ModelState.AddModelError("", $"Algo salio mal Actualizando el registro.{categoria.Nombre}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }


    }   
    
}
