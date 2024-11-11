using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tl2_tp5_2024_miguell29.Models;

namespace tl2_tp5_2024_miguell29.Repository
{
    public interface IPresupuestoRepository
    {
        public void CreatePresupuesto(Presupuesto presupuesto);
        public List<Presupuesto> GetPresupuestos();
        public Presupuesto GetPresupuestoById(int id);

        
    //! Originalmente este metodo debia recibir como parametro un id
    //! pero me parecio mas logico que tambien reciba el producto y la cantidad.
        public void AddProductToPresupuesto(int id , Producto producto, int cantidad);
        public void DeletePresupuesto(int id);
    }
}