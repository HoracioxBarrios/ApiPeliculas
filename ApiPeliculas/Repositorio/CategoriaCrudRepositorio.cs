using ApiPeliculas.Data;
using ApiPeliculas.Models;
using ApiPeliculas.Repositorio.IRepositorio;

namespace ApiPeliculas.Repositorio
{
    public class CategoriaCrudRepositorio : ICategoriaCrudRepositorio
    {
        private readonly ApplicationDbContext _bd;
        public CategoriaCrudRepositorio(ApplicationDbContext bd)
        {
            _bd = bd;
        }



        public bool CrearCategoria(Categoria categoria)
        {
            throw new NotImplementedException();
        }

        public ICollection<Categoria> GetCategorias()
        {
            throw new NotImplementedException();
        }

        public Categoria GetCategoria(int categoriaId)
        {
            throw new NotImplementedException();
        }

        public bool EditarCategoria(Categoria categoria)
        {
            categoria.FechaCreacion = DateTime.Now;
            _bd.Categorias.Update(categoria);
            return Guardar();
        }

        public bool BorrarCategoria(Categoria categoriaABorrar)
        {
            _bd.Categorias.Remove(categoriaABorrar);
            return Guardar();
        }



        public bool ExisteCategoria(int Id)
        {
            throw new NotImplementedException();
        }

        public bool ExisteCategoria(string nombre)
        {
            throw new NotImplementedException();
        }

        public bool Guardar()
        {
            throw new NotImplementedException();
        }
    }
}
