using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace ExamenPractico.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private InventarioContext _context;

        public StockController(InventarioContext context)
        {
            _context = context;
        }

        //Registrar nuevo stock
        [HttpPost]
        public async Task<ActionResult<List<Stock>>> AddStock(Stock stock)
        {       
            var articulo = await(from datos in _context.Articulos where datos.ArticuloId == stock.ArticuloId select datos).FirstOrDefaultAsync();
            var almacen = await (from datos in _context.Almacens where datos.AlmacenId == stock.AlmacenId select datos).FirstOrDefaultAsync();

            if (articulo == null || almacen == null)
            {
                var result = JsonSerializer.Serialize("Articulo/Almacen no existe");
                return BadRequest(result);
            }        

            _context.Stocks.Add(stock);
            await _context.SaveChangesAsync();

            return Ok(await _context.Stocks.ToListAsync());
        }
    }
}
