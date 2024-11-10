using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using tl2_tp5_2024_miguell29.Models;

namespace tl2_tp5_2024_miguell29.Repository
{
    public interface IProductoRepository
    {
        public void NewProduct(Producto producto);
        public void UpdateProduct(int id, Producto producto);
        public List<Producto> GetProducts();
        public Producto GetProductById(int id);
        public void DeleteProduct(int id);
    }

}