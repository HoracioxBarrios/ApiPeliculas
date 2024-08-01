using Microsoft.EntityFrameworkCore;
using ApiPeliculas.Models;
namespace ApiPeliculas.Data
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options) 
        {
            //Options se va a pasar al padre (DbContext)
        }


        //Aca Pasamos las Entidades (Modelos)
        public DbSet<Categoria> Categorias { get; set; }


    }
}
 