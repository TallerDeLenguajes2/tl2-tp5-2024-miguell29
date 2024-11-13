
using Microsoft.AspNetCore.Mvc;
using tl2_tp5_2024_miguell29.Models;
using tl2_tp5_2024_miguell29.Repository;

namespace tl2_tp5_2024_miguell29.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PresupuestoController : ControllerBase
    {
        private PresupuestoRepository _presupuestoRepository;
        public PresupuestoController()
        {
            _presupuestoRepository = new PresupuestoRepository();
        }

        [HttpGet]
        public ActionResult<List<Presupuesto>> Getpresupuestos()
        {
            return Ok(_presupuestoRepository.GetPresupuestos());
        }
        
        [HttpPost]
        public ActionResult CreatePresupuesto(Presupuesto presupuesto)
        {
            if (presupuesto == null) return BadRequest(presupuesto);
            _presupuestoRepository.CreatePresupuesto(presupuesto);
            return Created();
        }
        [HttpPost("{id}/ProductoDetalle")]
        public ActionResult AddProduct(int id, [FromBody] Producto producto, int cantidad)
        {
            _presupuestoRepository.AddProductToPresupuesto(id, producto, cantidad);
            return Created();
        }

    }
}