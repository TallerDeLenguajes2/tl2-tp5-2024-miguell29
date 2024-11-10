using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using tl2_tp5_2024_miguell29.Models;
using tl2_tp5_2024_miguell29.Repository;

namespace tl2_tp5_2024_miguell29.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : ControllerBase
    {
        private ProductoRepository _productoRepository;
        public ProductoController()
        {
            _productoRepository = new ProductoRepository();
        }




        [HttpGet]
        public ActionResult<List<Producto>> GetProductos()
        {
            var productos = _productoRepository.GetProducts();
            return Ok(productos);
        }
    }
}