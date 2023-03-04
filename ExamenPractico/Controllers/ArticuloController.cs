using Azure.Core;
using database;
using ExamenPractico.Middlewares;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLog;
using System.Text.Json;

namespace ExamenPractico.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticuloController : ControllerBase
    {
        private InventarioContext _context;

        public ArticuloController(InventarioContext context)
        {
            _context = context;
        }

        //Obtener lista de los articulos
        [HttpGet]
        public async Task<ActionResult<List<Articulo>>> Get()
        {
            var consulta = from datos in _context.Articulos select datos;

            return Ok(await consulta.ToListAsync());
        }

        //Obtener articulo por id
        [HttpGet("{id}")]
        public async Task<ActionResult<Articulo>> FindArticulo(int id)
        {
            var consulta =  await _context.Articulos.FindAsync(id);

            if (consulta == null)
            {
                var result = JsonSerializer.Serialize("Articulo no encontrado");
                return BadRequest(result);
            }

            return Ok(consulta);
        }

        //Registrar un articulo
        [HttpPost]
        public async Task<ActionResult<List<Articulo>>> AddArticulo(Articulo articulo)
        {
            _context.Articulos.Add(articulo);
            await _context.SaveChangesAsync();

            return Ok(await _context.Articulos.ToListAsync());
        }

        //Actualizar articulo existente por id
        [HttpPut]
        public async Task<ActionResult<List<Articulo>>> UpdateArticulo(Articulo request)
        {
            var put_articulo = await (from datos in _context.Articulos where datos.ArticuloId == request.ArticuloId select datos).FirstOrDefaultAsync();

            if (put_articulo == null)
            {
                var result = JsonSerializer.Serialize("El articulo a modificar no existe");
                return BadRequest(result);
            }

            put_articulo.Nombre = request.Nombre;
            put_articulo.Descripcion = request.Descripcion;
            put_articulo.Precio = request.Precio;

            await _context.SaveChangesAsync();

            return Ok(await _context.Articulos.ToListAsync());
        }

        //Eliminar articulo por id
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Articulo>>> Delete(int id)
        {
            var del_articulo = await(from datos in _context.Articulos where datos.ArticuloId == id select datos).FirstOrDefaultAsync();

            if (del_articulo == null)
            {
                var result = JsonSerializer.Serialize("El articulo a eliminar no existe");
                return BadRequest(result);
            }

            _context.Articulos.Remove(del_articulo);
            await _context.SaveChangesAsync();

            return Ok(await _context.Articulos.ToListAsync());
        }
    }
}
