using ApiPeliculas.Models;

namespace ApiPeliculas.Repositorio.IRepositorio
{
    public interface ICategoriaCrudRepositorio
    {
        bool CrearCategoria(Categoria categoria);
        ICollection<Categoria> GetCategorias();
        Categoria GetCategoria(int categoriaId);
        bool EditarCategoria(Categoria categoria);
        bool BorrarCategoria(Categoria categoriaABorrar);

        bool ExisteCategoria(int Id);
        bool ExisteCategoria(string nombre);

        bool Guardar();

    }
}
