using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tl2_tp5_2024_miguell29.Models
{
    public class Presupuesto
    {
        public int IdPresupuesto { get; set; }
        public string NombreDestinatario { get; set; }
        public List<PresupuestoDetalle> Detalle { get; set; }    
        public void MontoPresupuesto()
        {

        }
        public void MontoPresupuestoConIva()
        {
            
        }
        public void CantidadProductos()
        {
            
        }
    }
}