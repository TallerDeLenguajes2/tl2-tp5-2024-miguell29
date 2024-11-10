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
        public ActionResult<List<Producto>> GetProducts()
        {
            var productos = _productoRepository.GetProducts();
            return Ok(productos);
        }
        [HttpPost]
        public ActionResult CreateProduct(Producto producto)
        {
            _productoRepository.NewProduct(producto);
            return Ok();
        }
        [HttpPut("{id}")]
        public ActionResult UpdateProduct(int id,[FromBody] Producto producto)
        {
            _productoRepository.UpdateProduct(id, producto);
            return Ok();
        }
    }
}