using ApiPeliculas.Models.DataTransferObjects;
using ApiPeliculas.Repositorio.IRepositorio;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            var listaCategorias = _ctRepo.GetCategorias().ToList();

            var listaCategoriasDto = new List<CategoriaDto>();// Vamos a exponer el Dto en lugar de la Lista de Categorias directamente

            foreach(var categoria in listaCategorias)
            {
                listaCategoriasDto.Add(_mapper.Map<CategoriaDto>(categoria));
            }
            return Ok(listaCategoriasDto);
        }
    }
}
