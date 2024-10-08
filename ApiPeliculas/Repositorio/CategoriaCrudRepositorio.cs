﻿using ApiPeliculas.Data;
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
            categoria.FechaCreacion = DateTime.Now;
            _bd.Categorias.Add(categoria);
            return Guardar();
        }

        public ICollection<Categoria> GetCategorias()
        {
            return _bd.Categorias.OrderBy(c => c.Nombre).ToList();
        }

        public Categoria GetCategoria(int categoriaId)
        {
            return _bd.Categorias.FirstOrDefault(c => c.Id == categoriaId);
        }

        public bool ActualizarCategoria(Categoria categoria)
        {
            categoria.FechaCreacion = DateTime.Now;
            //Arreglamos el Problema del Put 
            var categoriaExistente = _bd.Categorias.Find(categoria.Id);
            if(categoriaExistente != null)
            {
                _bd.Entry(categoriaExistente).CurrentValues.SetValues(categoria);
            }
            else //Si existe
            {
                _bd.Categorias.Update(categoria);
            }
            return Guardar();
        }

        public bool BorrarCategoria(Categoria categoriaABorrar)
        {
            _bd.Categorias.Remove(categoriaABorrar);
            return Guardar();
        }



        public bool ExisteCategoria(int Id)
        {
            return _bd.Categorias.Any((c)=> c.Id == Id);
        }

        public bool ExisteCategoria(string nombre)
        {
            bool existe =_bd.Categorias.Any(
                (c)=> c.Nombre.ToLower().Trim() == nombre.ToLower().Trim());
            return existe;
        }

        public bool Guardar()
        {          
            bool seGuardo = (_bd.SaveChanges() > 0) ? true : false;
            return seGuardo;
        }
    }
}
